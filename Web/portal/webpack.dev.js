const merge = require('webpack-merge');
const path = require('path');
const webpack = require('webpack');
const common = require('./webpack.common.js');
module.exports = merge(common, {
    plugins: [
        new webpack.DefinePlugin({
            'process.env.NODE_ENV': JSON.stringify('dev')
        })
    ],
    devServer: {
        host: "0.0.0.0",
        port: 8080,
        index: 'Index.html',
        contentBase: path.resolve(__dirname, 'dist'),
        compress: true,
    },
    devtool: 'inline-source-map',
});