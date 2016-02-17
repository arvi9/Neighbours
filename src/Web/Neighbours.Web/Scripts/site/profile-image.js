function onSelect(e) {
    $.each(e.files, function (index, value) {
        readMultipleFiles(value);
    });
}

function onRemove(e) {
    $('#wrongFileMsg').hide();
    $("#radioGroup").find("input[type='radio']").each(function (index, el) {
        if (el.checked) {
            radioBtnImages(el)
        }
    })
}

function readMultipleFiles(file) {
    if (file.rawFile.type.indexOf('image') < 0) {
        $('#wrongFileMsg').show();
        return;
    }

    $('#wrongFileMsg').hide();
    var reader = new FileReader();
    reader.onload = function (e) {
        $('#imgShow').find("img").attr('src', e.target.result);
    }
    reader.readAsDataURL(file.rawFile);
}


function radioBtnImages(target) {

    var img = $('#imgShow').find("img");
    
    if (target.value === "Male") {
        img.attr('src', '../Content/imgs/male.png');
    }

    if (target.value === "Female") {
        img.attr('src', '../Content/imgs/female.png');
    }

    if (target.value === "Other") {
        img.attr('src', '../Content/imgs/other.png');
    }
}

$("#radioGroup").on("click", ":radio", function (e) {
    if (!($('.k-upload-empty').is('div'))) {
        return;
    }
    radioBtnImages(e.target);
})