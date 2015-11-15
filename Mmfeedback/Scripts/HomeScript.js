var loading = false;
        var page = 0;
        var searchQuery = "";
        var savedTags = [];
        var currentTagId = -1;
        var shouldClear = false;
        $(document).ready(function(){
	        $("#search").on("input", function(){
	            if (shouldClear){
	                $("#search").val("");
	                shouldClear = false;
	                return;
	            }
	            SuggestTags($("#search").val());
	            if (savedTags.length == 0 && $("#search").val() == "")
	                Search("", 0);
	        });
	    });
	    
	    function DeleteTag(object){
	        var tag = $("#" + object.parentNode.parentNode.id).attr("name");
	        var newTags = []
	        for (var i = 0; i < savedTags.length; i += 1){
	            if (savedTags[i] != tag)
	                newTags.push(savedTags[i]);
	        }
	        savedTags = newTags;
	        $("#" + object.parentNode.parentNode.id).remove();
	        if (savedTags.length == 0 && $("#search").val() == "")
	            Search("", 0);
	        else
	            Search("", 0);
	    }
	                 
	    function SaveTags(event){
	        var keyCode = event.which || event.keyCode;
	        if (keyCode == 44 || keyCode == 32){
	            Save();
	            return false;
	        }
	    }

	    function Save(){
	        var tagToSave = $("#search").val();
            Search(tagToSave, 0);
            savedTags.push(tagToSave);
            currentTagId += 1;
            $("#savedTags").append('<td name="' + tagToSave + '" id=tag' + currentTagId + '><span style="float: left"><button style="height: 20px; width: 20px" onclick="DeleteTag(this)">x</button></span>' + tagToSave + "</td>");
            $("#search").val("");
            shouldClear = true;
        }

	    function FillSearch(text){
	        $("#search").val(text);
	        Search(text, 0);
	        $("#suggestedTags").html("");
	        Save();
	    }

	    function SuggestTags(start){
	        if (start == ""){
	            $("#suggestedTags").html("");
	            return;
	        }
	        $.get("Home/GetFitTags/?start=" + start, function(data){
	            if (data != ""){
	                $("#suggestedTags").html("");
	                var suggested = data.split(',');
	                for (var i in suggested)
				        $("#suggestedTags").append("<td onclick='FillSearch(\"" + suggested[i] + "\")' name='" + suggested[i] + "'>" + suggested[i] + "</td>");
				}
			    else
					$("#suggestedTags").html("");
			});
		}

        function Search(query, searchPage){
			if (!loading){
				loading = true;
				page = searchPage;
			    if (savedTags.length == 0)
			        searchQuery = query;
			    else
			        searchQuery = savedTags.join(',') + ',' + query;
				//if (query == "")
				    //searchQuery = "";
				$("div#loading").html('<img src="@Url.Content("/App_LocalResources/loader.gif")"');
				$.get("Home/IndexSearch/?searchQuery=" + searchQuery + "&page=" + searchPage, function(data){
					if (data != '' && page == 0)
						$("#reviewsList").html(data);
				    else if (data != "")
				        $("#reviewsList").append(data);
					else if (searchPage == 0)
						$("#reviewsList").html("<p>Nothing found!</p>");
					loading = false;
					$("div#loading").empty();
				});
			}
		}

        function LoadNewContent(){
			if (page > -1 && !loading){
				loading = true;
				page += 1;
				$("div#loading").html('<img src="@Url.Content("/App_LocalResources/loader.gif")"');
				if (searchQuery != ""){
				    loading = false;
				    Search(searchQuery, page)
				    return;
				}
				$.get("Page" + page, function(data){
					if (data != '')
						$("#reviewsList").append(data);
					else
						page = -1;
					loading = false;
					$("div#loading").empty();
				});
			}
		}

		$(window).scroll(function(){
			if ($(window).scrollTop() == $(document).height() - $(window).height())
				LoadNewContent();
		});