

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
    <System.Runtime.CompilerServices.Extension()> _
    Public Function ToObservableCollection(Of T)(ByVal items As List(Of T)) As ObservableCollection(Of T)

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
    Public Function Exists(Of T)(ByVal this As T) As Boolean
        Try
            Return Not IsNothing(this)
        Catch ex As Exception
            Return False
        End Try
    End Function

    <System.Runtime.CompilerServices.Extension()>
    Public Sub Add(Of T)(ByRef arr As T(), ByVal ParamArray item() As T)
        Try
            For Each _item As T In item
                Array.Resize(arr, arr.Length + 1)
                arr(arr.Length - 1) = _item
            Next

        Catch ex As Exception
            If arr Is Nothing Then
                arr = item
            End If
        End Try

    End Sub

End Module