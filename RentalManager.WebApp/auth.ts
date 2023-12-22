import NextAuth from "next-auth";
import { authConfig } from "./auth.config";
import CredentialsProvider from "next-auth/providers/credentials";
import axios from "axios";
import {
	OpenAPI,
	UserService,
	UserWithRolesDto,
} from "@/app/lib/actions/apiClient/client";

OpenAPI.BASE = "http://localhost:8080";
// OpenAPI.WITH_CREDENTIALS = true;
// OpenAPI.CREDENTIALS = "include";

export const { auth, signIn, signOut } = NextAuth({
	...authConfig,
	providers: [
		CredentialsProvider({
			name: "Credentials",
			credentials: {
				username: { label: "Username", type: "text" },
				password: { label: "Password", type: "password" },
				rememberMe: { label: "RememberMe", type: "boolean" },
			},

			async authorize(credentials) {
				console.log(
					"cred",
					typeof credentials.username,
					typeof credentials.password,
					typeof credentials.rememberMe,
				);

				if (
					typeof credentials.username !== "string" ||
					typeof credentials.password !== "string" ||
					typeof credentials.rememberMe !== "string"
				) {
					throw new Error("Unable to parse credentials");
				}

				const requestUrl =
					OpenAPI.BASE +
					"/User/signIn?IsPersistent=" +
					credentials.rememberMe +
					"&UserName=" +
					credentials.username +
					"&Password=" +
					credentials.password;

				const result = await axios(requestUrl, {
					method: "post",
				});

				console.log(result);

				const headerCookie = result.headers["set-cookie"];

				if (headerCookie !== undefined) {
					OpenAPI.HEADERS = {
						Cookie: headerCookie[0],
					};
				}

				const user = await UserService.getUserWhoAmI();

				console.log(user);

				if (user.id === undefined) {
					return null;
				}

				return {
					id: user.id.toString(),
					name: user.name,
					surname: user.surname,
					image: user.image,
					roles: user.roles,
				};
			},
		}),
	],
	callbacks: {
		async session({ session, user, token }) {
			console.log("GOT");

			return session;
		},
	},
});

declare module "next-auth" {
	interface User extends UserWithRolesDto {}
}
