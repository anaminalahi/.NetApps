private void RunNightlyUpload()
{
	try
	{
		string outputDir = @"c:\BLOBSqlToJpeg\";
		string imageDir = Path.Combine(outputDir, "SavePicturesFolder\\");
		string csvFilePath = Path.Combine(outputDir, "Output.csv");

		try
		{
			Directory.CreateDirectory(imageDir);
			Directory.CreateDirectory(outputDir);
			LogToFile("Directories created.");
		}
		catch (Exception ex)
		{
			LogToFile($"Error creating directories: {ex.Message}");
			return;
		}

		// Initialize MongoDB connection
		myclient = new MongoClient(ConnectionString);
		mydatabase = myclient.GetDatabase(DatabaseName);
		mycollection = mydatabase.GetCollection<BsonDocument>(CollectionName);

		LogToFile("Nightly upload started");

		string query = @"
		SELECT 
			p.EMPID, 
			p.Firs, 
			p.LastName, 
			CONCAT(p.FirstName, ' ', p.LastName) AS FullName, 
			f.LASTCHANGED AS DeactivationDate, 
			f.LNL_BLOB AS BlobData, 
			f.FORMAT_IMAGE 
		FROM dbo.PERSONNEL p 
		INNER JOIN dbo.FILTEREDPICTURES f ON CAST(p.EMPID AS INT) = f.EMPID 
		ORDER BY p.EMPID";

		var csvBuilder = new StringBuilder();
		csvBuilder.AppendLine("ImageFilePath,store,caseNumber,reportedloss,expdate,action");

		int processedCount = 0;
		using (SqlConnection conn = new SqlConnection(connectionString))
		{
			conn.Open();
			using (SqlCommand command = new SqlCommand(query, conn))
			using (SqlDataReader reader = command.ExecuteReader())
			{
				MongoDbList = new List<MongoDbRowEnreg>();

				LogToFile("Executing query.");
				while (reader.Read())
				{
					try
					{
						string imageFileName = "";
						string fullImagePath = "";
						string imageFilePath = "";
						string expDate = "";
						string caseNumber = "";

						// Get the BLOB data
						byte[] blobData = null;
						string formatImage = null;

						int empId = Convert.ToInt32(reader["EMPID"]);
						string fullName = reader["FullName"] == DBNull.Value ? "" : (string)reader["FullName"];

						DateTime deactivationDate = reader["DeactivationDate"] == DBNull.Value ? DateTime.MinValue : (DateTime)reader["DeactivationDate"];
						blobData = reader["BlobData"] == DBNull.Value ? null : (byte[])reader["BlobData"];

						if (blobData != null)
						{
							formatImage = DetectBlobType(blobData);
							if (formatImage == "unknown")
							{
								LogToFile($"Unknown blob format for EMPID {empId}. Skipping.");
								continue; // Skip this record
							}
						}

						// Build the data for the CSV
						imageFileName = $"{empId}_0.jpeg";
						fullImagePath = Path.Combine(imageDir, imageFileName);
						File.WriteAllBytes(fullImagePath, blobData);

						LogToFile($"Saved image: {imageFileName}");
						imageFilePath = Path.Combine("c:\\BLOBSqlToJpeg\\SavePicturesFolder\\", imageFileName).Replace("\\", "/");
						expDate = deactivationDate.ToString("yyyy-MM-dd HH:mm:ss");
						caseNumber = $"{fullName}";

						string csvRow = $"{imageFilePath},SCSPA,{caseNumber},0,{expDate},No Action Needed";
						csvBuilder.AppendLine(csvRow);

						// Prepare MongoDB entry
						MongoDbRowEnreg uneLigne = new MongoDbRowEnreg
						{
							EmpID = empId,
							FullName = fullName,
							DeactivationDate = deactivationDate,
							BlobData = blobData,
							FormatImage = "jpeg"
						};

						// Upload to MongoDBList
						MongoDbList.Add(uneLigne);

						processedCount++;
					}
					catch (Exception ex)
					{
						LogToFile($"Error querying database: {ex.Message}\nStackTrace: {ex.StackTrace}");
						return;
					}
				}
			}
		}

		LogToFile($"Prepared upload list with {processedCount} items.");

		// Write CSV File
		try
		{
			File.WriteAllText(csvFilePath, csvBuilder.ToString(), Encoding.UTF8);
			LogToFile($"CSV file generated at: {csvFilePath}");
		}
		catch (Exception ex)
		{
			LogToFile($"Error writing CSV: {ex.Message}");
			return; // Still return count even if CSV fails
		}

		// Upload to MongoDB
		foreach (var mongoEntry in MongoDbList)
		{
			UploadToMongoDB(mongoEntry.EmpID, mongoEntry.FullName, mongoEntry.BlobData, mongoEntry.FormatImage, mongoEntry.DeactivationDate);
		}


		// Trigger the EXE
		string exePath = Path.Combine(outputDir, "FaceFirst.Tools.AutoEnroller.exe");
		if (File.Exists(exePath))
		{
			try
			{
				Process.Start(exePath);
				LogToFile($"Started {exePath}");
			}
			catch (Exception ex)
			{
				LogToFile($"Error starting EXE: {ex.Message}");
			}
		}
		else
		{
			LogToFile($"EXE not found at: {exePath}");
		}

		string successMsg = $"Nightly upload completed. Processed {processedCount} items.";
		LogToFile(successMsg);
	}
	catch (Exception ex)
	{
		LogToFile($"Error creating directories: {ex.Message}");
		return;
	}
} 

