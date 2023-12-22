import type { NextAuthConfig } from "next-auth";

export const authConfig = {
	pages: {
		signIn: "/login",
	},
	providers: [],
	callbacks: {
		authorized({ auth, request: { nextUrl } }) {
			const isLoggedIn = !!auth?.user;
			const isOnLogin = nextUrl.pathname == "/login";
			const isOnRegister = nextUrl.pathname == "/register";

			if (isLoggedIn && (isOnLogin || isOnRegister)) {
				return Response.redirect(new URL("/dashboard", nextUrl));
			}

			if (!isLoggedIn && (isOnRegister || isOnLogin)) {
				return Response.redirect(new URL("/login", nextUrl));
			}

			return true;
		},
	},
} satisfies NextAuthConfig;
