"use strict";
var path = require('path');
module.exports = {
    mode: 'development',
    entry: "./js/entry.jsx",
    output: {
        path: path.join(__dirname, './js/'),
        filename: 'bundle.js'
    },
    module: {
        rules: [
            {
                test: /\.jsx?$/,
                loaders: [
                    'babel-loader?presets[]=es2015,presets[]=react'
                ],
                exclude: /node_modules/
            },
        ]
    }
};