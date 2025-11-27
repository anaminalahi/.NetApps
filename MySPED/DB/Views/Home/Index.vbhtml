@Code
    ViewData("Title") = "Home Page"
End Code

<div class="jumbotron">
    <h1>ASP.NET</h1>
    <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS and JavaScript.</p>
    <p><a href="http://asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
</div>

@Code
    Html.Kendo().PanelBar() _
.Name("IntroPanelBar") _
.Items(Sub(items)
        items.Add() _
.Text("Getting Started") _
.Selected(True) _
.Expanded(True) _
.Content(Sub()
            @<text>
                <p>
                    ASP.NET MVC gives you a powerful, patterns-based way To build dynamic websites that
                    enables a clean separation of concerns And gives you full control over markup
                    For enjoyable, agile development.
                </p>
                <a Class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301865">Learn more &raquo;</a>
            </text>
         End Sub)
        items.Add() _
.Text("Get more libraries") _
.Content(Sub()
            @<text>
                <p> NuGet Is a free Visual Studio extension that makes it easy To add, remove, And update libraries And tools In Visual Studio projects.</p>
                <a Class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301866">Learn more &raquo;</a>
            </text>
         End Sub)
        items.Add() _
.Text("Web Hosting") _
.Content(Sub()
            @<text>
                <p> You can easily find a web hosting company that offers the right mix Of features And price For your applications.</p>
                <a Class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301867">Learn more &raquo;</a>
            </text>
         End Sub)
       End Sub).Render()
End Code
