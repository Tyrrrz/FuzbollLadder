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
                test: /\.js?$/,
                use: [
                  {
                    loader: 'babel-loader',
                    options: {
                      plugins: [
                        'transform-decorators-legacy',
                        'transform-class-properties',
                      ],
                    },
                  },
                ],
                exclude: /(node_modules)/,
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
            { test: /\.css$/, loader: "style-loader!css-loader" }
        ]
    },
    plugins: [
        new webpack.ProvidePlugin({
           $: "jquery",
           jQuery: "jquery"
       })
    ]
}