"use strict";

module.exports = {
    entry: "./js/react/Order.js",
    output: {
        filename: "./js/react/bundle.js"
    },
    module: {
        loaders: [
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