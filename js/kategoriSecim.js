function AltKategoriSec() {

    var katID = $('#aKat_id').val();

    $.ajax({
        url: "/Araclar/altkategoriler",
        type: "GET",
        dataType: "JSON",
        data: { id: katID },
        success: function (cevap) {

            $("#bKat_id").html("");

            $.each(cevap, function (i, Bkat) {

                $("#bKat_id").append(
                    $('<option></option>').val(Bkat.ID).html(Bkat.bKat));

            });

        }
    })
}