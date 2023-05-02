var UpdatedId = null;

function CartRequest(meth, wareid, qty) {
  $.ajax({
    url: UserAddressPrefix + '/' + meth + '/' + wareid + '?qty=' + qty,
    method: 'POST',
    data: { isInCart: IsInCartPage },
    success: function (Data, Status, jqXHR) {
      CartUpdateComplete(Data);
    },
  });
}

function CartQtyChanged(ev) {
  var $this = $(ev.target).closest('input[data-wareid]'); //currentTarget
  if (!$this.length) return;
  var wareid = $this.data('wareid');
  UpdatedId = wareid;
  CartRequest('set', wareid, $this.val());
}

function CartQtyClick(ev) {
  var $this = $(ev.target).closest('[data-wareid][data-change]'); //currentTarget
  if (!$this.length) return;
  var wareid = $this.data('wareid');
  UpdatedId = wareid;
  var delta = $this.data('change');

  CartRequest('change', wareid, delta);
}

function EmptyCart(ev) {
  CartRequest('emptyCart', '0', '0');
}
