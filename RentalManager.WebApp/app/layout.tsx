import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import React from "react";
import ThemeRegistry from "@/app/lib/ThemeRegistry/ThemeRegistry";
import RecoilProvider from "@/app/lib/RecoilProvider";
import { SessionProvider2 } from "@/app/lib/SessionProvider";

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
				<SessionProvider2>
					{/*<RecoilProvider>*/}
					<ThemeRegistry>{children}</ThemeRegistry>
					{/*</RecoilProvider>*/}
				</SessionProvider2>
			</body>
		</html>
	);
}
