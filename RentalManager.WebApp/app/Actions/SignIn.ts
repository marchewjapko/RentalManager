"use server";
import { LoginService, OpenAPI } from "@/app/ApiClient/codegen";
import { ApiResult } from "@/app/ApiClient/ApiResult";

OpenAPI.BASE = "http://localhost:8080";
OpenAPI.WITH_CREDENTIALS = true;
OpenAPI.CREDENTIALS = "include";

export default async function SignIn({
	userName,
	password,
	useCookies,
	useSessionCookies,
}: {
	userName: string;
	password: string;
	useCookies: boolean;
	useSessionCookies: boolean;
}): Promise<ApiResult> {
	try {
		await LoginService.postUserLogin({
			userName: userName,
			password: password,
			useCookies: useCookies,
			useSessionCookies: useSessionCookies,
		});
		return {
			isSuccess: true,
			title: null,
			message: null,
		};
	} catch (e: any) {
		return {
			isSuccess: false,
			title: e.body?.title,
			message: e.body?.detail,
		};
	}
}
