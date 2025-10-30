Imports System.Collections.ObjectModel

Partial Public Class Accessoire
    Public ReadOnly Property Visuel As ImageSource
        Get
            Dim oImg As ImageSource = Nothing
            If Not Me.ImageAccessoire Is Nothing Then
                oImg = ToImage(Me.ImageAccessoire)
            End If
            Return oImg
        End Get
    End Property

    Public ReadOnly Property MesVisuels As ObservableCollection(Of Accessoire_Visuel)
        Get
            Return GetVisuels(Me)
        End Get
    End Property

    Public ReadOnly Property MesCompatibles As ObservableCollection(Of Modele)
        Get
            Return GetCompatibles(Me)
        End Get
    End Property

    ReadOnly Property MesProprietes As ObservableCollection(Of Accessoire_Propriete)
        Get
            Return GetProprietes(Me)
        End Get
    End Property

    Public Function GetProprietes(ByVal Selected_Accessoire As Accessoire) As ObservableCollection(Of Accessoire_Propriete)
        Dim _nlstPropriete = Nothing
        Dim _lstPropriete = From oPropriete In DBContext.Accessoires_Proprietes
                            Where (oPropriete.IdAccessoire = Selected_Accessoire.IdAccessoire)
                            Order By oPropriete.Valeur
                            Select oPropriete
        If Not _lstPropriete Is Nothing AndAlso _lstPropriete.Any Then
            _nlstPropriete = _lstPropriete.ToList.ToObservableCollection
        End If
        Return _nlstPropriete
    End Function

    Public Function GetVisuels(ByVal Selected_Accessoire As Accessoire) As ObservableCollection(Of Accessoire_Visuel)
        Dim _nlstVisuels = Nothing
        Dim _lstVisuels = From oPropriete In DBContext.Accessoires_Visuels
                          Where (oPropriete.IdAccessoire = Selected_Accessoire.IdAccessoire)
                          Order By oPropriete.Sku
                          Select oPropriete
        If Not _lstVisuels Is Nothing AndAlso _lstVisuels.Any Then
            _nlstVisuels = _lstVisuels.ToList.ToObservableCollection
        End If
        Return _nlstVisuels
    End Function

    Public Function GetCompatibles(ByVal Selected_Accessoire As Accessoire) As ObservableCollection(Of Modele)

        Dim _nlstCompatible = Nothing

        Dim _lstCompatible = From oComp In DBContext.Compatibilites
                             Where (oComp.IdAccessoire = Selected_Accessoire.IdAccessoire) Select oComp


        If Not _lstCompatible Is Nothing AndAlso _lstCompatible.Any Then

            Dim _rq2 = From oTel In DBContext.Modeles, oComp In _lstCompatible
                       Where (oTel.IdModele = oComp.IdModele) Select oTel

            _nlstCompatible = _rq2.ToList.ToObservableCollection

        End If

        Return _nlstCompatible

    End Function

End Class
