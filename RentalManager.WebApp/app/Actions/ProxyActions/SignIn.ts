// "use server";
// import { OpenAPI } from "@/app/ApiClient/codegen";
// import { ApiResult } from "@/app/ApiClient/ApiResult";
// import axios from "axios";
// import { signIn } from "@/auth";
//
// OpenAPI.BASE = "http://localhost:8080";
// OpenAPI.WITH_CREDENTIALS = true;
// OpenAPI.CREDENTIALS = "include";
//
// export default async function SignIn({
// 	userName,
// 	password,
// 	useCookies,
// 	useSessionCookies,
// }: {
// 	userName: string;
// 	password: string;
// 	useCookies: boolean;
// 	useSessionCookies: boolean;
// }): Promise<ApiResult> {
// 	try {
// 		const requestUrl =
// 			OpenAPI.BASE +
// 			"/User/login?UseCookies=" +
// 			useCookies +
// 			"&UseSessionCookies=" +
// 			useSessionCookies +
// 			"&UserName=" +
// 			userName +
// 			"&Password=" +
// 			password;
//
// 		const result = await axios(requestUrl, {
// 			method: "post",
// 		});
//
// 		const headerCookie = result.headers["set-cookie"];
//
// 		if (headerCookie !== undefined) {
// 			OpenAPI.HEADERS = {
// 				Cookie: headerCookie[0],
// 			};
// 		}
//
// 		await signIn("credentials", {
// 			username: "my test",
// 		});
//
// 		return {
// 			isSuccess: true,
// 			title: "Login Successful",
// 			message: "You will soon be redirected",
// 		};
// 	} catch (e: any) {
// 		await signIn("credentials", {
// 			username: "my test",
// 		});
//
// 		return {
// 			isSuccess: false,
// 			title: e.response?.data?.title,
// 			message: e.response?.data?.detail,
// 		};
// 	}
// }
