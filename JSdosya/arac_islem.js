function aracSil(ID) {

    bootbox.confirm('aracı silmek istiyor musunuz?', function (cevap) {

        if (cevap) {

            $.ajax({
                url: '/Araclar/aracSil',
                data: { ID: ID },
                success: function (sonuc) {
                    bootbox.alert(sonuc);
                    $('#satir_' + ID).remove();
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

//function aracDegistir(id) {
//    var marka = document.getElementById('marka_' + id);
//    var model = document.getElementById('model_' + id);
//    var durum = document.getElementById('durumlar_' + id);
//    var plaka = document.getElementById('plaka_' + id);
//    var yas = document.getElementById('yas_' + id);
//    var yil = document.getElementById('yil_' + id);
//    var vites = document.getElementById('vites_' + id);
//    var yakit = document.getElementById('yakit_' + id);

//    if ((marka.value != '') && (model.value != '') && (plaka.value != '') && (yakit.value != '') && (vites.value != '') && (yil.value != '')) {

 
//        $.ajax({

//            url: '/Araclar/aracDegistir',
//            type: 'POST',
//            data: { id: id, marka: marka.value, model: model.value, plaka: plaka.value, yakit: yakit.value, vites: vites.value, yas: yas.value, yil: yil.value },
//            success: function (cevap) {

//                if (cevap == "Yetkisiz giriş") {

//                    Window.location = "/Admin/login";

//                }
//                else {
//                    bootbox.alert(cevap);
//                }
//                bootbox.alert('Bilgilerinz Güncellendi..')
//            }
//        })
//    }
//    else {
//        bootbox.alert('Alanları boş bırakmayınız!');
//    }
//}


function aracDegistir(id) {

    var marka = document.getElementById('marka_' + id);
    var model = document.getElementById('model_' + id);
    var yakit = document.getElementById('yakit_' + id);
    var vites = document.getElementById('vites_' + id);
    var plaka = document.getElementById('plaka_' + id);
    var yil = document.getElementById('yil_' + id);
    var durum = document.getElementById('durumlar_' + id);

    if ((marka.value != '') && (model.value != '') && (yakit.value != '') && (vites.value != '') && (plaka.value != '') &&  (yil.value != '')) {

        $.ajax({
            url: '/Araclar/aracDegis',
            type: 'POST',
            data: { id: id, marka: marka.value, model: model.value, yakit: yakit.value, vites: vites.value, plaka: plaka.value, yil: yil.value
            },
            success: function (cevap) {

                if (cevap == "Yetkisiz giriş") {
                    Window.location = "/Admin/login";
               }
               else {
                   bootbox.alert(cevap);
                }

               
            },
            error: function () {
                bootbox.alert('Hata 89');
            }
        })
    }
}

function imajYenile(id) {

    var formData = new FormData();
    var imaj = document.getElementById('imaj_' + id);
    var logo = document.getElementById('logo_' + id);

    formData.append(imaj.files[0].name, imaj.files[0]);

    formData.append("id", id);

    var xhr = new XMLHttpRequest();

    xhr.open('POST', '/Araclar/imaj_degistir');
    xhr.send(formData);
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status == 200) {

            logo.src = '/imajlar/' + imaj.files[0].name;
            bootbox.alert("İmaj Güncellendi.");

        }
    }

}