const path = require('path');
const webpack = require('webpack');
module.exports = {
	entry: {
		app: [
			path.join(__dirname, 'app', 'main.js')
		]
	},
	output: {
        path: path.join(__dirname, 'dist'),
        publicPath: '/',
		filename: 'bundle.js'
    },
    devtool: "source-map",
    watch: true,
    devServer: {
        inline: true,
        port: 3333
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                loader: 'babel-loader',
                query: {
                    presets: ['es2015', 'react']
                },
            },
            {
                test: /\.scss$/,
                exclude: /node_modules/,
                use: [
                    'style-loader',
                    'css-loader',
                    'sass-loader'
                ]
            },
        ]
    },
    plugins: [
        new webpack.ProvidePlugin({
           $: "jquery",
           jQuery: "jquery"
       })
    ]
}