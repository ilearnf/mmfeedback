function stringStartsWith(base, prefix){
		    return base.slice(0, prefix.length) == prefix;
		}
		var userName = "";
		var userUrl = "";
		var markedTags = [];
		$(document).ready(function(){
		    $("#search").on("input", function(){
		        $(".tagCheckbox").each(function(){
		            if (stringStartsWith($(this).val(), $("#search").val()))
		                $(this).parent().parent().show();
		            else
		                $(this).parent().parent().hide();
		        });
	            if ($(".checkbox:visible").length == 0)
	                $("#addTag").css("visibility", "visible");
	            else
	                $("#addTag").css("visibility", "hidden");
		    });
		    $("#addTag").on("click", function(){
		        var newTag = $("#search").val();
		        var newCheckbox = '<tr><td><div class="checkbox"><label><input class="tagCheckbox" type="checkbox"' + 
			        'value="' + newTag + '" /><span>' + newTag + '</span></label></div></td></tr>';
		        $("#tagsToAdd").append(newCheckbox);
		    });
		    $("#sign").on("click", function(){
		        window.open("/Act/VkLogin", "login", "width=800, heght=600, location=0", true);
		        return false;
		    });
		    $("#form").on("submit", function(){
		        $(".tagCheckbox:checked").each(function(){
		            markedTags.push($(this).val());
		        });
		        var valid = true;
		        $("#tagsTextBox").val(markedTags.join(','));
		        $(".required").each(function(){
		            if ($(this).val() == ""){
		                $(this).after("<span class='alert'>Required!</span>");
		                $(this).addClass("error");
		                valid = false;
		            }
			        setTimeout(function(){
		                $(".alert").each(function(){
		                    $(this).fadeOut("fast");
		                    $(".error").each(function(){
		                        $(this).removeClass("error");
		                    });
		                });
		            }, 4000);
		        });
		        return valid;
		    });         
		});