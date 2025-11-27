$("#RenvoiOuCreation").click(function () {

    var c = this.checked;
    if (c) {

        $("#passwordChangeForm").hide(1000);
    } else {

        $("#passwordChangeForm").show(1000);
    }
})