const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const TransferWebpackPlugin = require('transfer-webpack-plugin');
module.exports = {
    entry: {
        Index: './src/pages/Index/Index.ts',
        Login: './src/pages/Login/Login.ts'
    },
    plugins: [
        new CleanWebpackPlugin(),
        new HtmlWebpackPlugin({
            filename: 'Index.html',
            chunks: ['Index'],
            template: './src/pages/Index/Index.html'
        }),
        new HtmlWebpackPlugin({
            filename: 'Login.html',
            chunks: ['Login'],
            template: './src/pages/Login/Login.html'
        }),
        new TransferWebpackPlugin([
            { from: 'fonts', to: 'static/fonts' }
        ], path.resolve(__dirname, "src"))
    ],
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: 'ts-loader',
                exclude: /node_modules/
            },
            {
                test: /\.css$/,
                use: [
                    'style-loader',
                    'css-loader'
                ],
                exclude: /node_modules/
            },
            {
                test: /\.(woff|woff2|eot|ttf|otf)$/,
                use: [
                    'file-loader'
                ]
            }
        ]
    },
    resolve: {
        extensions: ['.tsx', '.ts', '.js']
    },
    output: {
        path: path.resolve(__dirname, 'dist'),
        filename: '[name].[chunkhash].js'
    }
};