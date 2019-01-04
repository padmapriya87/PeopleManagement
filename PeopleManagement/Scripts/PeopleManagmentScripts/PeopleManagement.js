function searchPeople() {
    var firstName = $("#fnTextbox").val();
    var lastName = $("#lnTextbox").val();
    var dob = $("#DOBTextBox").val();
    var gender = $("#genderDropdown").val();
    var stateId = $("#statesDropDown").val();
    var person =  { "FirstName": firstName, "LastName": lastName, "Gender": gender, "DOB": dob , "StateId": stateId};
   // $.get("", person, JSON);
    $.ajax({
        url: "/People/GetPeople",
        type: 'GET',
        dataType: 'json', // added data type
        data: person,
        success: function (outputData) {
           
            $("tr[id=dataRow]").remove();
            $.each(outputData, function (index, person) {
                var personsjson = JSON.stringify(person);
                var rowdiv =  "<tr  id='dataRow'>" +"<td>" + person.FirstName + "</td>" +
                    "<td>" + person.LastName + " </td>" +
                    "<td>" + person.Gender + "</td>" +
                    "<td>" + person.StateCode + "</td>" +
                    "<td>" + new Date(parseInt(person.DOB.replace(/[^0-9 +]/g, ''))).toLocaleString().slice(0, 10) + "</td>" +
                    "<td>" + "<button  class='btn btn-primary' id='editButton' onclick='ShowModal(" + personsjson+");'>Edit</button>" + "</td>" + "</tr>";
                $("#PersonList").append(rowdiv);
                
            });
        }
    });
}

function ShowModal(person) {
    $("#AddPersonModal").modal("show");
    $("#saveButton").unbind("click");
    if (person !=null) {
        $(".modal-title").text("Edit Person");
         $("#firstName").val(person.FirstName);
         $("#lastName").val(person.LastName);
        $("#dob").val(new Date(parseInt(person.DOB.replace(/[^0-9 +]/g, ''))).toISOString().split('T')[0]);
         $("#gender").val(person.Gender);
        $("#state").val(person.StateId);
        $("#saveButton").click(function (e) {
            SavePerson(person.PersonId);
            e.preventDefault();
        });
    }
    else {
        $(".modal-title").text("Add Person");
        $("#firstName").val("");
        $("#lastName").val("");
        $("#dob").val("");
        $("#gender").val("");
        $("#state").val(1);
        $("#saveButton").click(function (e) {
            SavePerson(0);
            e.preventDefault();
        });
    }

}
function SavePerson(personId)
{
    var validator = $("#addPersonForm").validate();
    validator.form();
    if (validator.checkForm()) {
        var firstName = $("#firstName").val();
        var lastName = $("#lastName").val();
        var dob = $("#dob").val();
        var gender = $("#gender").val();
        var stateId = $("#state").val();
        var person = { "PersonId": personId, "FirstName": firstName, "LastName": lastName, "Gender": gender, "DOB": dob, "StateId": stateId };
        $.ajax({
            url: "/People/Upsert",
            type: 'POST',
            dataType: 'json',
            data: person,
            success: function (outputData) {
                if (outputData > 0) {
                    $("#AddPersonModal").modal("hide");
                }
            }
        });
    }
}

$(document).ready(function () {
    //on page load display all persons
    searchPeople();
    //add validations with in modal
    $('#addPersonForm').validate({
        rules: {
            firstName: {
                maxlength: 50,
                required: true
            },
            lastName: {
                maxlength: 50,
                required: true
            },
            gender: {
                required: true
            },
            dob: {
                required: true
            },
        },
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }
    });

});




