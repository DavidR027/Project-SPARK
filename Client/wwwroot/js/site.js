// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//========= glightbox
GLightbox({
    'href': 'https://www.youtube.com/watch?v=Gxw45q3Ga3k',
    'type': 'video',
    'source': 'youtube', //vimeo, youtube or local
    'width': 900,
    'autoplayVideos': true,
});

//========= testimonial 
tns({
    container: '.testimonial-slider',
    items: 3,
    slideBy: 'page',
    autoplay: false,
    mouseDrag: true,
    gutter: 0,
    nav: true,
    controls: false,
    controlsText: ['<i class="lni lni-arrow-left"></i>', '<i class="lni lni-arrow-right"></i>'],
    responsive: {
        0: {
            items: 1,
        },
        540: {
            items: 1,
        },
        768: {
            items: 2,
        },
        992: {
            items: 2,
        },
        1170: {
            items: 3,
        }
    }
});

const finaleDate = new Date("February 15, 2023 00:00:00").getTime();

const timer = () => {
    const now = new Date().getTime();
    let diff = finaleDate - now;
    if (diff < 0) {
        document.querySelector('.alert').style.display = 'block';
        document.querySelector('.container').style.display = 'none';
    }

    let days = Math.floor(diff / (1000 * 60 * 60 * 24));
    let hours = Math.floor(diff % (1000 * 60 * 60 * 24) / (1000 * 60 * 60));
    let minutes = Math.floor(diff % (1000 * 60 * 60) / (1000 * 60));
    let seconds = Math.floor(diff % (1000 * 60) / 1000);

    days <= 99 ? days = `0${days}` : days;
    days <= 9 ? days = `00${days}` : days;
    hours <= 9 ? hours = `0${hours}` : hours;
    minutes <= 9 ? minutes = `0${minutes}` : minutes;
    seconds <= 9 ? seconds = `0${seconds}` : seconds;

    document.querySelector('#days').textContent = days;
    document.querySelector('#hours').textContent = hours;
    document.querySelector('#minutes').textContent = minutes;
    document.querySelector('#seconds').textContent = seconds;

}
timer();
setInterval(timer, 1000);
