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
                url: "/Admin/PostReview",
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
            var userId = $(this).parent().find(".author-id").text();
            var reason = $(this).parent().find(".reason").val();
            $.ajax({
                url: '/Admin/DeclineReview',
                type: "POST",
                data: { 
                    id: $(this).parent().find(".id").text(),
                    userId: userId,
                    reason: reason
                },
                success: function(result){
                    alert(result);
                }
            });
            $(this).parent().hide();
        });
    });