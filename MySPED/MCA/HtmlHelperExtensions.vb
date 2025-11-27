Imports System.Runtime.CompilerServices

Namespace RazorExtensions
    Module HtmlHelperExtensions

        <Extension()>
        Public Function DisplayForProperty(helper As HtmlHelper) As MvcHtmlString
            Return MvcHtmlString.Create("TEST")
        End Function

        <Extension>
        Function [If](value As MvcHtmlString, evaluation As Boolean) As MvcHtmlString
            Return If(evaluation, value, MvcHtmlString.Empty)
        End Function

    End Module

End Namespace

