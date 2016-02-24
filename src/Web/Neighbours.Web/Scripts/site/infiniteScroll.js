var page = 0,
    inCallback = false,
    hasReachedEndOfInfiniteScroll = false;

var scrollHandler = function () {
    if (hasReachedEndOfInfiniteScroll == false &&
            ($(window).scrollTop() + $(window).height() > $(document).height()-5)) {
        loadMoreToInfiniteScrollTable(moreRowsUrl);
    }
}

function loadMoreToInfiniteScrollTable(loadMoreRowsUrl) {
    if (page > -1 && !inCallback) {
        inCallback = true;
        page++;
        $("div#loading").show();
        setTimeout(function () {
        
        $.ajax({
            type: 'GET',
            url: loadMoreRowsUrl,
            data: "pageNum=" + page,
            
            success: function (data, textstatus){ 
                var dataRep = data.replace(/(?:\r\n|\r|\n)/g, '');;
                if (data != '' && dataRep != '') {
                    console.log(dataRep);
                    $("div.infinite-scroll").append(data);
                }
                else {
                    page = -1;
                }

                inCallback = false;
                $("div#loading").hide();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
            }
        });
        }, 1000)
    } else {
        hasReachedEndOfInfiniteScroll = true;
    }
}

function showNoMoreRecords() {
    hasReachedEndOfInfiniteScroll = true;
}