////(function () {
  
////    var _$form = $('#RegisterForm');
////    debugger
////    $.validator.addMethod("customUsername", function (value, element) {
////        if (value === _$form.find('input[name="EmailAddress"]').val()) {
////            return true;
////        }

////        //Username can not be an email address (except the email address entered)
////        return !$.validator.methods.email.apply(this, arguments);
////    }, abp.localization.localize("RegisterFormUserNameInvalidMessage", "Charismatic"));

////    _$form.validate({
////        rules: {
////            UserName: {
////                required: true,
////                customUsername: true
////            }
////        }
////    });
////})();
$(document).ready(function () {

    $("#RegisterForm").validate({
        rules: {
            Name: "required"
        },
        messages: {
            Name: "Please specify your name"

        }
    })

    $('#btn').click(function () {
        $("#form1").validate();  // This is not working and is not validating the form
    });

});
//$(document).ready(function () {
//    $('#RegisterButton').click(function () {
//        debugger
//        var Selecte = document.getElementById('example').selectedOptions;
//        var index = Selecte.length;
//        //if (index == null || index == 0) {
//        //    alert("Please select at least one Specialty")
//        //}
//        var html = '<tr name="movementsDetailRow"> <td>'
//        html += '<select  multiple = "multiple" class="form-control"  name="SpecialtyList" > ';
//        for (var i = 0; i < index; i++) {
//            var x = Selecte[i].value;
//            var name = Selecte[i].innerHTML;
//            html += '<option  name="SpecialtyList[' + i + '].Name" value = "' + x + '"></option>';
//        }
//        html += '</select></td> </tr>';
//        $('#datatableEnter').append(html);

//    });

//})
