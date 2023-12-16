import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import ContextRegistry from "./ContextRegistry";
import React from "react";
import { OpenAPI } from "@/app/ApiClient/codegen";

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
	title: "Create Next App",
	description: "Generated by create next app",
};

export default function RootLayout({
	children,
}: {
	children: React.ReactNode;
}) {
	return (
		<html lang="en">
			<body className={inter.className}>
				<ContextRegistry options={{ key: "mui-theme" }}>
					{children}
				</ContextRegistry>
			</body>
		</html>
	);
}
