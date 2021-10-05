function bilgiDegis(id) {

    var email = document.getElementById('email_' + id);
    var sifre = document.getElementById('sifre_' + id);


    if ((email.value != "") && (sifre.value != "")) {


        $.ajax({

            url: '/Admin/degisim',
            type: 'POST',
            data: {
                id: id, email: email.value, sifre: sifre.value
            },
            success: function (cevap) {
                bootbox.alert(cevap)
            },
            error: function () {
                bootbox.alert('Sistem Hatası 20')
            }
        })
    }

}