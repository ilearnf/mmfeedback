$(document).ready(function(){
        $(".confirm").on("click", function(){
            var parent = $(this).parent();
            parent.hide();
            var description = parent.find(".description").text();
            var title = parent.find(".title").text();
            var tags = parent.find(".tags").text();
            var author = parent.find(".author").text();
            var authorId = parent.find(".author-id").text();
            var id = parent.find(".id").text();
            $.ajax({
                url: '@Url.Action("PostReview", "Admin")',
                type: "POST",
                data: { 
	                description: description,
	                title: title,
	                author: author,
	                authorId: authorId,
	                id: id,
	                tags: tags
                },
                success: function(result){
                    alert(result);
                }
            });
        });
        $(".decline").on("click", function(){
            $(this).parent().hide();
            $.ajax({
                url: '@Url.Action("DeclineReview", "Admin")',
                type: "POST",
                data: { 
                    id: $(this).parent().find(".id").text(),
                },
                success: function(result){
                    alert(result);
                }
            });
        });
    });