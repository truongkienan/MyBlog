function customSwal(d) {
    if (d > 0) {
        swal({
            title: 'Update success',
            text: 'Waitting...',
            icon: 'success',
            timer: 800,
            buttons: false,
        })
    }
    else {
        swal('Update failed');
    }
}