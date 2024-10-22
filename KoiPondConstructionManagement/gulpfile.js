//var gulp = require('gulp');
//const sass = require('gulp-sass')(require('sass'));
//var cssmin = require("gulp-cssmin");
//var concat = require("gulp-concat");
//var uglify = require('gulp-uglify'); // Để minify JS
//var rename = require('gulp-rename'); // Để đổi tên file minified

//var paths = {
//    scss: "./wwwroot/**/*.scss", // Bao gồm tất cả các file SCSS trong wwwroot
//    css: "./wwwroot/**/*.css", // Bao gồm tất cả các file CSS trong wwwroot
//    js: "./wwwroot/**/*.js", // Bao gồm tất cả các file JS trong wwwroot
//    minJs: "./wwwroot/**/*.min.js", // Để loại bỏ các file JS đã minified
//};

//// Task to compile SCSS to CSS, add autoprefixer, and minify
//gulp.task('sass', function () {
//    return gulp
//        .src(paths.scss)
//        .pipe(sass().on('error', sass.logError)) // Compile SCSS và xử lý lỗi
//        .pipe(gulp.dest('./wwwroot')) // Lưu file CSS biên dịch vào wwwroot
//        .pipe(cssmin()) // Minify CSS
//        .pipe(rename({ suffix: '.min' })) // Đổi tên file thành .min.css
//        .pipe(gulp.dest('./wwwroot')); // Lưu file minified CSS
//});

//// Task to minify and concat JavaScript
//gulp.task('scripts', function () {
//    return gulp
//        .src([paths.js, '!' + paths.minJs]) // Lấy tất cả file JS trừ các file .min.js
//        .pipe(concat('site.min.js')) // Gộp tất cả JS lại thành một file duy nhất
//        .pipe(uglify()) // Minify file JS
//        .pipe(gulp.dest('./wwwroot')); // Lưu file minified JS vào wwwroot
//});

//// Watch SCSS and JS files for changes
//gulp.task('watch', function () {
//    gulp.watch(paths.scss, gulp.series('sass')); // Theo dõi thay đổi SCSS
//    gulp.watch(paths.js, gulp.series('scripts')); // Theo dõi thay đổi JS
//});

//// Default task to run both 'sass', 'scripts', and 'watch'
//gulp.task('default', gulp.series('sass', 'scripts', 'watch'));

var gulp = require('gulp');
var uglify = require('gulp-uglify');
var rename = require('gulp-rename');

// var paths = {
//     js: "wwwroot/admin/js/sb-admin-2.js", // File JS cần minify
//     minJsDest: "wwwroot/admin/js/" // Thư mục lưu file minified
// };
var paths = {
    js: "wwwroot/js/ckeditor5/ckeditor.js",
    minJsDest: "wwwroot/js/ckeditor5/"
};

// Task để minify file sb-admin-2.js
gulp.task('minify-js', function () {
    return gulp
        .src(paths.js) // Lấy file JS cần minify
        .pipe(uglify()) // Minify file JS
        .pipe(rename({ suffix: '.min' })) // Đổi tên thành sb-admin-2.min.js
        .pipe(gulp.dest(paths.minJsDest)); // Lưu vào thư mục đích
});

