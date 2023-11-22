$(document).ready(function () {

    let showLessBtn = $(".show-less button")

    let initialProducts = $(".parent-elem").children().length //4


    $(document).on("click", ".show-more button", function () {

        let parent = $(".parent-elem")
        let skipCount = $(parent).children().length
        let productCount =$(parent).attr("data-count")

        $.ajax({
            url: `shop/showmore?skipcount=${skipCount}`,
            type: "Get",
            success: function (res) {

                $(parent).append(res)

                skipCount = $(parent).children().length

                if (skipCount >= productCount) {
                    $(".show-more button").addClass("d-none")
                    $(showLessBtn).removeClass("d-none")
                }

            }
        })

        $(document).on("click", ".show-less button", function () {

            

            $.ajax({
                url: `shop/showless`,
                type: "Get",
                success: function (res) {
                    $(parent).html(res)

                    $(".show-more button").removeClass("d-none")
                    $(showLessBtn).addClass("d-none")

                }

            })




        })


    })

    








})