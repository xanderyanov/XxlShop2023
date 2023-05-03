$(function () {
  $('.brandSection__title').on('click', function () {
    var $this = $(this);
    var contentCaption = $this
      .closest('.brandSection')
      .find('.brandSection__contentCaption');
    var content = $this.closest('.brandSection').find('.brandSection__content');
    if ($this.hasClass('open')) {
      $this.removeClass('open');
      contentCaption.slideUp();
      content.removeClass('brandSection__content_open');
    } else {
      $this.addClass('open');
      contentCaption.slideDown();
      content.addClass('brandSection__content_open');
    }
  });
});
