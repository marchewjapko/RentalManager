"use client";
import React, { useState } from "react";
import createCache from "@emotion/cache";
import { useServerInsertedHTML } from "next/navigation";
import { CacheProvider } from "@emotion/react";
import { ThemeProvider } from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";
import { createTheme } from "@mui/material";
import { Options } from "@emotion/cache";
import { RecoilRoot } from "recoil";
import ErrorSnackbarProvider from "@/app/SnackbarErrors/ErrorSnackbarProvider";

export const theme = createTheme({
	palette: {
		mode: "dark",
		primary: {
			main: "#3b82f6",
		},
	},
	shape: {
		borderRadius: 10,
	},
});

export default function ContextRegistry(props: {
	options: Options;
	children: React.ReactNode;
}) {
	const { options, children } = props;
	const [{ cache, flush }] = useState(() => {
		const cache = createCache(options);
		cache.compat = true;
		const prevInsert = cache.insert;
		let inserted: string[] = [];
		cache.insert = (...args) => {
			const serialized = args[1];
			if (cache.inserted[serialized.name] === undefined) {
				inserted.push(serialized.name);
			}
			return prevInsert(...args);
		};
		const flush = () => {
			const prevInserted = inserted;
			inserted = [];
			return prevInserted;
		};
		return { cache, flush };
	});
	useServerInsertedHTML(() => {
		const names = flush();
		if (names.length === 0) {
			return null;
		}
		let styles = "";
		for (const name of names) {
			styles += cache.inserted[name];
		}
		return (
			<style
				key={cache.key}
				data-emotion={`${cache.key} ${names.join(" ")}`}
				dangerouslySetInnerHTML={{
					__html: styles,
				}}
			/>
		);
	});
	return (
		<RecoilRoot>
			<CacheProvider value={cache}>
				<ThemeProvider theme={theme}>
					<CssBaseline />
					<ErrorSnackbarProvider />
					{children}
				</ThemeProvider>
			</CacheProvider>
		</RecoilRoot>
	);
}
