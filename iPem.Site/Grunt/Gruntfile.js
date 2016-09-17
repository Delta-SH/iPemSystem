'use strict';

module.exports = function (grunt) {

  require('load-grunt-tasks')(grunt);

  grunt.initConfig({
    pkg: grunt.file.readJSON('package.json'),
    concat: {
      options: {
        separator: '\n\n'
      },
      js_ux: {
        src: ['../Scripts/ux/**/*.js'],
        dest: '../Scripts/ux/all.js'
      },
      js_global: {
        src: ['../Scripts/global/icons.js','../Scripts/global/global.js','../Scripts/global/template.js'],
        dest: '../Scripts/global/all.js'
      },
      js_components: {
        src: ['../Scripts/components/**/*.js'],
        dest: '../Scripts/components/all.js'
      },
      js_build: {
        src: ['../Scripts/ux/all.js','../Scripts/global/all.js','../Scripts/components/all.js'],
        dest: '../Scripts/all.js'
      },
      css_ux: {
        src: ['../Content/ux/**/*.css'],
        dest: '../Content/ux/all.css'
      }
    }
  });

  // 默认任务
  grunt.registerTask('default', ['concat']);
};