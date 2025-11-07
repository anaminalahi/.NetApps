using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace BLOBSqlToJpeg
{
    public class MongoUploader
    {
        private readonly string _conn;
        private readonly string _dbName;
        private readonly string _colName;
        private Assembly _driverAssembly;
        private Assembly _bsonAssembly;

        public MongoUploader(string conn, string dbName, string colName)
        {
            _conn = conn;
            _dbName = dbName;
            _colName = colName;

            TryLoadAssemblies();
        }

        private void TryLoadAssemblies()
        {
            try
            {
                _driverAssembly = Assembly.Load("MongoDB.Driver");
                _bsonAssembly = Assembly.Load("MongoDB.Bson");
            }
            catch
            {
                _driverAssembly = null;
                _bsonAssembly = null;
            }
        }

        private void EnsureAssemblies()
        {
            if (_driverAssembly == null || _bsonAssembly == null)
            {
                throw new InvalidOperationException("MongoDB driver assemblies not found. Ensure MongoDB.Driver NuGet package is installed.");
            }
        }

        public async Task<bool> CheckDatabaseExistsAsync(string dbName)
        {
            EnsureAssemblies();

            try
            {
                var clientType = _driverAssembly.GetType("MongoDB.Driver.MongoClient");
                dynamic client = Activator.CreateInstance(clientType, new object[] { _conn });

                var listDbsMethod = clientType.GetMethod("ListDatabaseNames");
                dynamic databaseNames = await (dynamic)listDbsMethod.Invoke(client, new object[] { });

                foreach (string name in databaseNames)
                {
                    if (name == dbName) return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking database existence: {ex.Message}");
                return false;
            }
        }

        public async Task CreateDatabaseAsync(string dbName)
        {
            EnsureAssemblies();

            try
            {
                var clientType = _driverAssembly.GetType("MongoDB.Driver.MongoClient");
                dynamic client = Activator.CreateInstance(clientType, new object[] { _conn });

                // Create database by creating a collection in it
                var getDbMethod = clientType.GetMethod("GetDatabase");
                dynamic database = getDbMethod.Invoke(client, new object[] { dbName });

                var createColMethod = database.GetType().GetMethod("CreateCollection");
                await (dynamic)createColMethod.Invoke(database, new object[] { "_init" });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating database: {ex.Message}", ex);
            }
        }

        public async Task<bool> CheckCollectionExistsAsync(string dbName, string colName)
        {
            EnsureAssemblies();

            try
            {
                var clientType = _driverAssembly.GetType("MongoDB.Driver.MongoClient");
                dynamic client = Activator.CreateInstance(clientType, new object[] { _conn });

                var getDbMethod = clientType.GetMethod("GetDatabase");
                dynamic database = getDbMethod.Invoke(client, new object[] { dbName });

                var listColsMethod = database.GetType().GetMethod("ListCollectionNames");
                dynamic collectionNames = await (dynamic)listColsMethod.Invoke(database, new object[] { });

                foreach (string name in collectionNames)
                {
                    if (name == colName) return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking collection existence: {ex.Message}");
                return false;
            }
        }

        public async Task CreateCollectionAsync(string dbName, string colName)
        {
            EnsureAssemblies();

            try
            {
                var clientType = _driverAssembly.GetType("MongoDB.Driver.MongoClient");
                dynamic client = Activator.CreateInstance(clientType, new object[] { _conn });

                var getDbMethod = clientType.GetMethod("GetDatabase");
                dynamic database = getDbMethod.Invoke(client, new object[] { dbName });

                var createColMethod = database.GetType().GetMethod("CreateCollection");
                await (dynamic)createColMethod.Invoke(database, new object[] { colName });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating collection: {ex.Message}", ex);
            }
        }

        public async Task CreateIndexAsync(string dbName, string colName, string field, bool unique)
        {
            EnsureAssemblies();

            try
            {
                var clientType = _driverAssembly.GetType("MongoDB.Driver.MongoClient");
                dynamic client = Activator.CreateInstance(clientType, new object[] { _conn });

                var getDbMethod = clientType.GetMethod("GetDatabase");
                dynamic database = getDbMethod.Invoke(client, new object[] { dbName });

                var bsonDocType = _bsonAssembly.GetType("MongoDB.Bson.BsonDocument");
                var getColMethod = database.GetType().GetMethod("GetCollection").MakeGenericMethod(bsonDocType);
                dynamic collection = getColMethod.Invoke(database, new object[] { colName });

                // Create index definition
                var indexKeysDoc = Activator.CreateInstance(bsonDocType);
                var addMethod = bsonDocType.GetMethod("Add", new Type[] { typeof(string), typeof(object) });
                addMethod.Invoke(indexKeysDoc, new object[] { field, 1 });

                // Create index options
                var indexOptionsType = _driverAssembly.GetType("MongoDB.Driver.CreateIndexOptions");
                dynamic indexOptions = Activator.CreateInstance(indexOptionsType);
                indexOptions.Unique = unique;

                var createIndexMethod = collection.GetType().GetMethod("CreateIndex");
                await (dynamic)createIndexMethod.Invoke(collection, new object[] { indexKeysDoc, indexOptions });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating index: {ex.Message}", ex);
            }
        }

        public async Task ClearCollectionAsync()
        {
            EnsureAssemblies();

            try
            {
                var clientType = _driverAssembly.GetType("MongoDB.Driver.MongoClient");
                dynamic client = Activator.CreateInstance(clientType, new object[] { _conn });

                var getDbMethod = clientType.GetMethod("GetDatabase");
                dynamic database = getDbMethod.Invoke(client, new object[] { _dbName });

                var dbType = database.GetType();
                var getColMethod = dbType.GetMethod("GetCollection", new Type[] { typeof(string) });
                var bsonDocType = _bsonAssembly.GetType("MongoDB.Bson.BsonDocument");
                var genericGetCol = getColMethod.MakeGenericMethod(bsonDocType);
                dynamic collection = genericGetCol.Invoke(database, new object[] { _colName });

                var emptyFilter = Activator.CreateInstance(bsonDocType);
                var deleteManyMethod = collection.GetType().GetMethod("DeleteMany", new Type[] { bsonDocType });
                var task = (Task)deleteManyMethod.Invoke(collection, new object[] { emptyFilter });
                await task;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing collection {_colName}: {ex.Message}");
            }
        }

        public async Task UploadImageAsync(byte[] bytes, string filename, string contentType, Dictionary<string, object> meta = null)
        {
            if (_driverAssembly == null || _bsonAssembly == null)
            {
                Console.WriteLine("MongoDB driver not found at runtime. Skipping upload for: " + filename);
                return;
            }

            try
            {
                var clientType = _driverAssembly.GetType("MongoDB.Driver.MongoClient");
                var client = Activator.CreateInstance(clientType, new object[] { _conn });

                var getDbMethod = clientType.GetMethod("GetDatabase", new Type[] { typeof(string) });
                object database = getDbMethod.Invoke(client, new object[] { _dbName });

                var dbType = database.GetType();
                var getColMethod = dbType.GetMethod("GetCollection", new Type[] { typeof(string) });
                var bsonDocType = _bsonAssembly.GetType("MongoDB.Bson.BsonDocument");
                var genericGetCol = getColMethod.MakeGenericMethod(bsonDocType);
                object collection = genericGetCol.Invoke(database, new object[] { _colName });

                var bsonBinaryType = _bsonAssembly.GetType("MongoDB.Bson.BsonBinaryData");
                var bsonValueType = _bsonAssembly.GetType("MongoDB.Bson.BsonValue");
                var bsonDoc = Activator.CreateInstance(bsonDocType);
                var addMethod = bsonDocType.GetMethod("Add", new Type[] { typeof(string), bsonValueType });
                var createMethod = bsonValueType.GetMethod("Create", new Type[] { typeof(object) });

                // Set _id
                object bvId = createMethod.Invoke(null, new object[] { filename });
                addMethod.Invoke(bsonDoc, new object[] { "_id", bvId });

                object bvFilename = createMethod.Invoke(null, new object[] { filename });
                addMethod.Invoke(bsonDoc, new object[] { "filename", bvFilename });

                object bvContentType = createMethod.Invoke(null, new object[] { contentType });
                addMethod.Invoke(bsonDoc, new object[] { "contentType", bvContentType });

                var bvData = Activator.CreateInstance(bsonBinaryType, new object[] { bytes });
                var bvDataAsValue = createMethod.Invoke(null, new object[] { bvData });
                addMethod.Invoke(bsonDoc, new object[] { "data", bvDataAsValue });

                object bvDate = createMethod.Invoke(null, new object[] { DateTime.UtcNow });
                addMethod.Invoke(bsonDoc, new object[] { "createdAt", bvDate });

                if (meta != null)
                {
                    foreach (var kv in meta)
                    {
                        var bv = createMethod.Invoke(null, new object[] { kv.Value });
                        addMethod.Invoke(bsonDoc, new object[] { kv.Key, bv });
                    }
                }

                // InsertOne
                var insertOne = collection.GetType().GetMethod("InsertOne");
                if (insertOne != null)
                {
                    var task = (Task)insertOne.Invoke(collection, new object[] { bsonDoc });
                    await task;
                    Console.WriteLine("Uploaded image to MongoDB: " + filename);
                }
                else
                {
                    throw new InvalidOperationException("Unable to find InsertOne method on MongoDB collection");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Mongo upload failed for {filename}: {ex.Message}", ex);
            }
        }

        // Keep old sync method for backward compatibility
        public void UploadImage(byte[] bytes, string filename, string contentType, Dictionary<string, object> meta = null)
        {
            UploadImageAsync(bytes, filename, contentType, meta).GetAwaiter().GetResult();
        }

        public async Task UploadDocumentAsync(Dictionary<string, object> data)
        {
            if (_driverAssembly == null || _bsonAssembly == null)
            {
                Console.WriteLine("MongoDB driver not found at runtime. Skipping document upload.");
                return;
            }

            try
            {
                var clientType = _driverAssembly.GetType("MongoDB.Driver.MongoClient");
                var client = Activator.CreateInstance(clientType, new object[] { _conn });

                var getDbMethod = clientType.GetMethod("GetDatabase", new Type[] { typeof(string) });
                object database = getDbMethod.Invoke(client, new object[] { _dbName });

                var dbType = database.GetType();
                var getColMethod = dbType.GetMethod("GetCollection", new Type[] { typeof(string) });
                var bsonDocType = _bsonAssembly.GetType("MongoDB.Bson.BsonDocument");
                var genericGetCol = getColMethod.MakeGenericMethod(bsonDocType);
                object collection = genericGetCol.Invoke(database, new object[] { _colName });

                var bsonValueType = _bsonAssembly.GetType("MongoDB.Bson.BsonValue");
                var bsonDoc = Activator.CreateInstance(bsonDocType);
                var addMethod = bsonDocType.GetMethod("Add", new Type[] { typeof(string), bsonValueType });
                var createMethod = bsonValueType.GetMethod("Create", new Type[] { typeof(object) });

                object bvDate = createMethod.Invoke(null, new object[] { DateTime.UtcNow });
                addMethod.Invoke(bsonDoc, new object[] { "createdAt", bvDate });

                foreach (var kv in data)
                {
                    var bv = createMethod.Invoke(null, new object[] { kv.Value });
                    addMethod.Invoke(bsonDoc, new object[] { kv.Key, bv });
                }

                // InsertOne
                var insertOne = collection.GetType().GetMethod("InsertOne");
                if (insertOne != null)
                {
                    var task = (Task)insertOne.Invoke(collection, new object[] { bsonDoc });
                    await task;
                    Console.WriteLine("Uploaded document to MongoDB with _id: " + data["_id"]);
                }
                else
                {
                    throw new InvalidOperationException("Unable to find InsertOne method on MongoDB collection");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Mongo document upload failed: {ex.Message}", ex);
            }
        }

        public void UploadDocument(Dictionary<string, object> data)
        {
            UploadDocumentAsync(data).GetAwaiter().GetResult();
        }
    }
}