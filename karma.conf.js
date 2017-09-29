﻿// Karma configuration for Angular testing

module.exports = function (config) {
    var webpackConfig = require('./config//webpack.test.js');

    var configuration = {
        //plugins: [
        //    //require('karma-trx-reporter'),
        //    require('karma-phantomjs-launcher') 
        //],
        // base path that will be used to resolve all patterns (eg. files, exclude)
        basePath: '',

        // frameworks to use
        // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
        frameworks: ['jasmine'],               

        // list of files / patterns to load in the browser
        files: [
            { pattern: './config/spec.bundle.js', watched: false }
        ],

        // list of files to exclude
        exclude: [
        ],

        // preprocess matching files before serving them to the browser
        // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
        preprocessors: {
            './config/spec.bundle.js': ['webpack', 'sourcemap']
        },

        // webpack
        webpack: webpackConfig,

        webpackServer: {
            noInfo: true
        },


        // test results reporter to use
        // possible values: 'dots', 'progress'
        // available reporters: https://npmjs.org/browse/keyword/karma-reporter
        reporters: ['progress', 'trx','spec'],
        //reporters: config.angularCli && config.angularCli.codeCoverage
        //    ? ['trx', 'progress', 'spec']
        //    : ['trx', 'progress'],
        trxReporter: { outputFile: 'test-results.trx', shortTestName: false },

        // web server port
        port: 9876,


        // enable / disable colors in the output (reporters and logs)
        colors: true,


        // level of logging
        // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
        logLevel: config.LOG_INFO,


        // enable / disable watching file and executing tests whenever any file changes
        autoWatch: true,


        // start these browsers
        // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
        //browsers: ['Chrome'],
        browsers: ['PhantomJS'],

        // Continuous Integration mode
        // if true, Karma captures browsers, runs the tests and exits
        singleRun: true

    };

    config.set(configuration);

}
