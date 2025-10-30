Imports System.IO
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data


Module ModuleMain

#Region "MEMBRES PARTAGES"

    'http://www.aspsnippets.com/Articles/Read-and-Write-BLOB-Data-to-SQL-Server-database-using-C-and-VBNet.aspx

    Public DBContext As dbgestelacEntities

    Public fPrincipale As FormMain

    Public LesModeles As IEnumerable(Of Modele)
    Public LesAccessoires As IEnumerable(Of Accessoire)
    Public LesModProp As IEnumerable(Of Modele_Propriete)
    Public LesAcProp As IEnumerable(Of Accessoire)
    Public LesStatuts As IEnumerable(Of Statut)
    Public LesMarques As IEnumerable(Of Marque)
    Public LesCategories As IEnumerable(Of Categorie)

    Public ImageAAJouter As ImageSource

#End Region


#Region "METHODES PARTAGEES"

    Sub Sablier(ByVal ActifON As Boolean)

        If ActifON Then
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait
        Else
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow
        End If

    End Sub

    Public Function ToImage(array As Byte()) As BitmapImage
        Using ms = New System.IO.MemoryStream(array)
            Dim image = New BitmapImage()
            image.BeginInit()
            image.CacheOption = BitmapCacheOption.OnLoad
            ' here
            image.StreamSource = ms
            image.EndInit()
            Return image
        End Using
    End Function

#End Region

End Module
