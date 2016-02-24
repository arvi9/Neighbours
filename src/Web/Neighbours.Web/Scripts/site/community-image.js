function onSelect(e) {
    $.each(e.files, function (index, value) {
        readMultipleFiles(value);
    });
}

function onRemove(e) {
    $('#wrongFileMsg').hide();
    $('#imgShow').find("img").attr('src', '/Content/imgs/default-cover.jpg');
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


