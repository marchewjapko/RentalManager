import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import LanguageDetector from 'i18next-browser-languagedetector';
import XHR from 'i18next-http-backend';

import generalEN from './Tanslations/generalEN.json';
import clientEN from './Tanslations/ClientTranslations/clientEN.json';
import employeeEN from './Tanslations/EmployeeTranslations/employeeEN.json';
import equipmentEN from './Tanslations/EquipmentTranslations/equipmentEN.json';
import agreementEN from './Tanslations/AgreementTranslations/agreementEN.json';
import paymentEN from './Tanslations/PaymentTranslations/paymentEN.json';
import generalPL from './Tanslations/generalPL.json';
import clientPL from './Tanslations/ClientTranslations/clientPL.json';
import employeePL from './Tanslations/EmployeeTranslations/employeePL.json';
import equipmentPL from './Tanslations/EquipmentTranslations/equipmentPL.json';
import agreementPL from './Tanslations/AgreementTranslations/agreementPL.json';
import paymentPL from './Tanslations/PaymentTranslations/paymentPL.json';
import drawerEN from './Tanslations/DrawerTranslations/drawerEN.json';
import drawerPL from './Tanslations/DrawerTranslations/drawerPL.json';

i18n.use(XHR)
	.use(LanguageDetector)
	.use(initReactI18next)
	.init({
		interpolation: {
			escapeValue: false,
		},
		supportedLngs: ['pl', 'en'],
		resources: {
			en: {
				generalTranslation: generalEN,
				clientTranslation: clientEN,
				employeeTranslation: employeeEN,
				equipmentTranslation: equipmentEN,
				agreementTranslation: agreementEN,
				paymentTranslation: paymentEN,
				drawerTranslation: drawerEN,
			},
			pl: {
				generalTranslation: generalPL,
				clientTranslation: clientPL,
				employeeTranslation: employeePL,
				equipmentTranslation: equipmentPL,
				agreementTranslation: agreementPL,
				paymentTranslation: paymentPL,
				drawerTranslation: drawerPL,
			},
		},
		debug: false,
	});

export default i18n;
