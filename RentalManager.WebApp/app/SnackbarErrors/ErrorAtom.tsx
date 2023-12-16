import { atom, DefaultValue, RecoilState } from "recoil";

export const errorAtom = atom({
	key: "errorAtom",
	default: {
		message: null as string | null,
	},
});
