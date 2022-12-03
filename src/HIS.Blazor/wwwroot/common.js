window.ShowToastr = (type, message) => {
    if (type == "success") {
        toastr.success(message, "Operation Successfull", { timeOut: 10000 });
    }
    if (type == "error") {
        toastr.error(message, "Operation Failed", { timeOut: 10000 });
    }
}


function OnClickButton() {
    var checked = $('.isDefaultIndicator').is(':checked');
    var button = $('btnSubmitLookUpForm').attr('type');

    if (checked && button == "button") {
        $('.isSystemIndicatorModal').modal('show');
    }

}

function OnLoadPage() {

    //var checked = $('.isDefaultIndicator').is(':checked');

    //if (checked) {
    $('.btnSubmitLookUpForm').attr('type', 'submit');
    $('.btnSubmitLookUpForm').attr('data-bs-toggle', '');
    $('.btnSubmitLookUpForm').attr('data-bs-target', '');

    /*    }*/

}

function BtnNoModal() {
    $('.isSystemIndicatorModal').modal('hide');
}

function OnChangeCheckBoxIsDefaultIndicator() {

    var checked = $('.isDefaultIndicator').is(':checked');

    if (checked) {
        $('.btnSubmitLookUpForm').attr('type', 'button');
        $('.btnSubmitLookUpForm').attr('data-bs-toggle', 'modal');
        $('.btnSubmitLookUpForm').attr('data-bs-target', '.isSystemIndicatorModal');
    }
    else {
        $('.btnSubmitLookUpForm').attr('type', 'submit');
        $('.btnSubmitLookUpForm').attr('data-bs-toggle', '');
        $('.btnSubmitLookUpForm').attr('data-bs-target', '');
    }

}


function InitSelect2() {

    $(".select2").select2({

        placeholder: 'Select An Option',
        allowClear: true
    });



}

//window.select2Component = {
//    init: function (className) {
//        //Initialize Select2 Elements
//        $('.' + className).select2({
//            placeholder: 'انتخاب کنید ...',
//            allowClear: true
//        });
//    },

//}

