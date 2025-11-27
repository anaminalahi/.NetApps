Imports System.Runtime.CompilerServices
Imports System.Collections.ObjectModel


<Extension()> Module Extensions

    ''' <summary>
    ''' 'Converts a generic System.Collections.Generic.IEnumerable(Of T) to a generic System.Collections.ObjectModel.ObservableCollection(Of T)
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="items"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()>
    Public Function ToObservableCollection(Of T)(ByVal items As List(Of T)) As ObservableCollection(Of T)

        Dim collection As New ObservableCollection(Of T)()

        For Each item As T In items
            collection.Add(item)
        Next

        Return collection
    End Function

    <System.Runtime.CompilerServices.Extension()>
    Public Function ToObservableCollectionFromEnumerable(Of T)(ByVal items As IEnumerable(Of T)) As ObservableCollection(Of T)

        Dim collection As New ObservableCollection(Of T)()

        For Each item As T In items
            collection.Add(item)
        Next

        Return collection
    End Function

    <System.Runtime.CompilerServices.Extension()>
    Function PourAccess(Of T)(ByVal m_valeur As T) As String
        Dim szValeur = String.Empty

        If m_valeur Is Nothing Then Return szValeur

        Select Case Type.GetTypeCode(m_valeur.GetType())

            Case TypeCode.Decimal, TypeCode.Double, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64
                szValeur = m_valeur.ToString.Trim

            Case TypeCode.String, TypeCode.Char
                szValeur = m_valeur.ToString.Replace("'", "''").Trim
        End Select

        Return szValeur
    End Function

    <System.Runtime.CompilerServices.Extension()>
    Function NulleOuNon(Of T)(ByVal m_valeur As T) As String

        Dim szValeur = String.Empty

        If m_valeur Is Nothing Then Return szValeur

        Select Case Type.GetTypeCode(m_valeur.GetType())

            Case TypeCode.Decimal, TypeCode.Double, TypeCode.Int16, TypeCode.Int32, TypeCode.Int64
                szValeur = m_valeur.ToString.Trim

            Case TypeCode.String, TypeCode.Char
                szValeur = m_valeur.ToString.Trim.ToUpper

            Case TypeCode.DateTime
                szValeur = Date.Parse(m_valeur.ToString).ToShortDateString

        End Select

        Return szValeur
    End Function

    <System.Runtime.CompilerServices.Extension()>
    Function TailléPourGespere(Of T)(ByVal m_valeur As T, ByVal taille As Integer) As String
        Dim szValeur = String.Empty

        If m_valeur Is Nothing Then Return szValeur

        szValeur = Mid(m_valeur.ToString.Trim, 1, taille)

        Return szValeur
    End Function

    <System.Runtime.CompilerServices.Extension()>
    Function FileNotIsInUse(ByVal path As String) As Boolean

        Dim isFree = True
        Dim peut As Boolean

        Try
            Dim fs As System.IO.FileStream = New System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate)

            If Not fs Is Nothing Then
                peut = fs.CanRead
                peut = fs.CanWrite
            End If

        Catch ex As System.IO.IOException
            isFree = False
        End Try

        Return isFree

    End Function

    <System.Runtime.CompilerServices.Extension()>
    Function Between(Of TSource, TResult As IComparable(Of TResult))(source As IEnumerable(Of TSource), selector As Func(Of TSource, TResult), lowest As TResult, highest As TResult) As IEnumerable(Of TSource)
        Return source.OrderBy(selector).SkipWhile(Function(s) selector.Invoke(s).CompareTo(lowest) < 0).TakeWhile(Function(s) selector.Invoke(s).CompareTo(highest) <= 0)
    End Function

End Module