$('INPUT[type="file"]').change(function () {
    var ext = this.value.match(/\.(.+)$/)[1];
    if (this.files[0].size > 3000000) {
        alert('Dopuszczalne są tylko pliki o wielkości do 3 MB');
        this.value = '';
        return;
    }
    switch (ext.toLowerCase()) {
        case 'pdf':
        case 'doc':
            break;
        default:
            alert('Dopuszczalne są tylko pliki typu pdf oraz doc');
            this.value = '';
    }
});