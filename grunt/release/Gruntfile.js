'use strict';

module.exports = function (grunt) {

  require('load-grunt-tasks')(grunt);

  grunt.initConfig({
    pkg: grunt.file.readJSON('package.json'),
    uglify: {
      options: {
        beautify: {
            max_line_len : 0 //(默认32K字节）- 在32K字节处增加换行符，传入0禁用此功能。
        },
        stripBanners: true,
        mangle: true, //混淆变量名
        preserveComments: false, //删除注释
        banner:'/*!\n* <%=pkg.name%> JavaScript Library v<%=pkg.version%>\n*\n* Copyright 2017, <%=pkg.author%>\n*\n* Date: <%=grunt.template.today("yyyy/mm/dd") %>\n*/\n'
      },
      build:{
        files:[
          {
            expand:true,
            cwd:'Release/Scripts/',
            src:['*.js','!*.min.js','!_references.js'],
            dest:'Release/tmp/Scripts/',
            ext: '.js',
            extDot: 'last'
          }
        ]
      }
    },
    cssmin:{
      options:{
        keepSpecialComments: 0 //* 保留全部注释（默认）, 1 仅保留第一条注释, 0 不保留注释
      },
      ux:{
        files:[
          {
            expand:true,
            cwd:'Release/Content/ux/',
            src:'all.css',
            dest:'Release/tmp/Content/ux/',
            ext: '.css',
            extDot: 'last'
          }
        ]
      },
      themes:{
        files:[
          {
            expand:true,
            cwd:'Release/Content/themes/css/',
            src:['*.css','!*.min.css'],
            dest:'Release/tmp/Content/themes/css/',
            ext: '.css',
            extDot: 'last'
          }
        ]
      },
      build:{
        files:[
          {
            expand:true,
            cwd:'Release/Content/',
            src:['*.css','!*.min.css'],
            dest:'Release/tmp/Content/',
            ext: '.css',
            extDot: 'last'
          }
        ]
      }
    },
    clean: {
      init: {
        src: ['Release/tmp/']
      },
      build: {
      	src: [
      	'Release/Content/*.css',
      	'Release/Content/themes/css/',
      	'Release/Content/ux/*.css',
      	'Release/Scripts/components/',
      	'Release/Scripts/global/',
      	'Release/Scripts/ux/',
      	'Release/Scripts/*.js',
      	]
      }
    },
    copy: {
      css:{
        files: [
          {
            expand: true,
            cwd: 'Release/tmp/Content/', 
            src: '**/*', 
            dest: 'Release/Content/'
          }
        ]
      },
      js:{
        files: [
          {
            expand: true,
            cwd: 'Release/tmp/Scripts/', 
            src: '**/*', 
            dest: 'Release/Scripts/'
          }
        ]
      }
    }
  });

  // 默认任务
  grunt.registerTask('default', ['clean:init','uglify','cssmin','clean:build','copy','clean:init']);
};