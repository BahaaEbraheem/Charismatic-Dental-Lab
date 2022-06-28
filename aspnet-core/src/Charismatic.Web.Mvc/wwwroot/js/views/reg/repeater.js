jQuery.fn.extend({
    createRepeater: function (options = {}) {
        var hasOption = function (optionKey) {
            return options.hasOwnProperty(optionKey);
        };

        var option = function (optionKey) {
            return options[optionKey];
        };

        var generateId = function (string) {
            return string
                .replace(/\[/g, '_')
                .replace(/\]/g, '')
                .toLowerCase();
        };


        var DeleteItem = function (items, key, fresh = true) {
            debugger

            var itemContent = items;
            var group = itemContent.data("group");
            var item = itemContent;
            var input = item.find('input,select,textarea');
           /* if (key != $('.items').length) {*/
             
           
        }

        var addItem = function (items, key, fresh = true) {
            debugger
            var itemContent = items;
            var group = itemContent.data("group");
            var item = itemContent;
            var input = item.find('input,select,textarea');
           
            
            input.each(function (index, el) {
                var attrName = $(el).data('name');
                var skipName = $(el).data('skip-name');
                if (skipName != true) {
                    $(el).attr("name", group + "[" + key + "]." + attrName );
                } else {
                    if (attrName != 'undefined') {
                        $(el).attr("name", attrName);
                    }
                }
                if (fresh == true) {
                    $(el).attr('value', '');
                }
                $(el).attr('id', generateId($(el).attr('name')));
                $(el).parent().find('label').attr('for', generateId($(el).attr('name')));
            })
            var itemClone = items;
            /* Handling remove btn */
            var removeButton = itemClone.find('.repeater-remove-btn');
            if (key == 0) {
                removeButton.attr('disabled', true);
            } else {
                removeButton.attr('disabled', false);
            }
            //removeButton.attr('onclick', '$(this).parents(\'.items\').remove()');
            removeButton.attr('onclick', 'bb()');
            var newItem = $("<div class='items'>" + itemClone.html() + "<div/>");
            newItem.attr('data-index', key)
            newItem.appendTo(repeater);
        };
        /* find elements */
        var repeater = this;
        var items = repeater.find(".items");
        var key = 0;
        var addButton = repeater.find('.repeater-add-btn');
        var removeButton = repeater.find('.repeater-remove-btn');
        items.each(function (index, item) {
            items.remove();
            if (hasOption('showFirstItemToDefault') && option('showFirstItemToDefault') == true) {
                addItem($(item), key);
                key++;
            } else {
                if (items.length > 1) {
                    addItem($(item), key);
                    key++;
                }
            }
        });
        /* handle click and add items */
        addButton.on("click", function () {
            var ItemCount = $('.items').length;
            key = ItemCount;
            addItem($(items[0]), key);
        });
        $('#repeater').on('click', 'button[onclick="bb()"]', function (e) {
            debugger
            var key = parseInt($(this).parents('.items').attr('data-index'));
            $(this).parents('.items').remove();
            resetIndexes();
           /* DeleteItem($(items[key]), key);*/
        })
    }
});

        
function resetIndexes() {
    var ItemCount = $('.items').length;
    var index = 0;
    $(".items").each(function (index, item) {
        debugger
        $(item).attr('data-index', index);
       
        var row = $(item).find('.form-control')
        $(row[0]).attr('name', "CenterList[" + index + "].Name");
        $(row[0]).attr('id', "centerlist_" + index + ".name");

        $(row[1]).attr('name', "CenterList[" + index + "].Country.Id");
        $(row[1]).attr('id', "centerlist_" + index + ".Country.Id");
        $(row[2]).attr('name', "CenterList[" + index + "].Building");
        $(row[2]).attr('id', "centerlist_" + index + ".Building");
        $(row[3]).attr('name', "CenterList[" + index + "].Street");
        $(row[3]).attr('id', "centerlist_" + index + ".Street");
        $(row[4]).attr('name', "CenterList[" + index + "].City");
        $(row[4]).attr('id', "centerlist_" + index + ".City");
        $(row[5]).attr('name', "CenterList[" + index + "].AccountEmail");
        $(row[5]).attr('id', "centerlist_" + index + ".AccountEmail");
        index++;
    //    key = index;
    })
}

