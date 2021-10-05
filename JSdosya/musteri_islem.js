function musteriSil(ID) {

    bootbox.confirm('aracı silmek istiyor musunuz?', function (cevap) {

        if (cevap) {
            $.ajax({
                url: '/Musteri/musteriSil',
                data: { ID: ID },
                success: function (cevap) {
                    if (cevap == "Yetkisiz giriş") {
                        Window.location = "/Admin/login";
                    }
                    else {
                        bootbox.alert(cevap);
                    }
                },
                error: function () {
                }
            });
        }
        else {
            bootbox.alert('Silme işlemi iptal edildi.');
        }
    })
}

function musteriDegistir(id) {

    var adSoyad = document.getElementById('ad_' + id);
    var dogum = document.getElementById('Dogum_' + id);
    var ehliyet = document.getElementById('ehliyet_' + id);
    var email = document.getElementById('email_' + id);
    var sifre = document.getElementById('sifre_' + id);
    var telefon = document.getElementById('telefon_' + id);
    var sehir = document.getElementById('sehir_' + id);
    var adres = document.getElementById('adres_' + id);
    var ilce = document.getElementById('ilce_' + id); 

    if ((adSoyad.value != '') && (dogum.value != '') && (ehliyet.value != '') && (email.value != '') && (sifre.value != '') && (telefon.value != '') && (sehir.value != '') && (adres.value != '') && (ilce.value != '')) {
   
        $.ajax({
            url: '/Musteri/musteriDegisim',
            data: { id: id, adSoyad: adSoyad.value, dogum: dogum.value, ehliyet: ehliyet.value, email: email.value, sifre: sifre.value, telefon: telefon.value, sehir: sehir.value, adres: adres.value,  ilce: ilce.value },
            type: 'POST',
            success: function (cevap) {

                if (cevap == "Yetkisiz giriş") {
                    Window.location = "/Admin/login";
                }
                else {
                    bootbox.alert(cevap);
                }
               
            },
            error: function () {
            }            
        })
    }
    else {
        bootbox.alert('Alanları boş bırakmayınız!');
    }
}


