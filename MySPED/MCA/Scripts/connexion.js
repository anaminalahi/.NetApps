$("#motDePasseoublie").click(function () {

    var c = this.checked;
    if (c) {
        $("#formConnexion").hide(1000);
        $("#rememberMe").hide(1000);
    } else {
        $("#formConnexion").show(1000);
        $("#rememberMe").show(1000);
    }
})