@Code
    ViewData("Title") = "About"
End Code

<hgroup class="title">
    <h1>@ViewBag.Title.</h1>
    <h2>@ViewBag.Message</h2>
</hgroup>

<article>
    <p>
        Use this area to provide additional information.
    </p>

    <p>
        Use this area to provide additional information.
    </p>

    <p>
        Use this area to provide additional information.
    </p>
</article>

<aside>
    <h3>UI for ASP.NET MVC TreeView</h3>
    <p>
        Use this area to provide additional information.
    </p>

    @(Html.Kendo().TreeView() _
        .Name("LinksTreeView") _
        .Items(Sub(items)
            items.Add() _
                .Text("Home") _
                .Action("Index", "Home")
            items.Add() _
                .Text("About") _
                .Action("About", "Home")
            items.Add() _
                .Text("Contact") _
                .Action("Contact", "Home")
        End Sub)
    )

</aside>
