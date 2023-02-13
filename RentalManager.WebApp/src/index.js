import React from 'react';
import ReactDOM from 'react-dom/client';
import * as serviceWorker from './serviceWorker';
import App from './App';

const renderReactDom = () => {
	const root = ReactDOM.createRoot(document.getElementById('root'));
	root.render(<App />);
};

if (window.cordova) {
	document.addEventListener(
		'deviceready',
		() => {
			renderReactDom();
		},
		false
	);
} else {
	renderReactDom();
}
serviceWorker.unregister();
