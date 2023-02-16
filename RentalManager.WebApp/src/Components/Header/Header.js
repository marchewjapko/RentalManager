import {
	AppBar,
	Box,
	Divider,
	Drawer,
	IconButton,
	List,
	Paper,
	Stack,
	Toolbar,
	Typography,
} from '@mui/material';
import { useState } from 'react';
import { useTheme } from '@mui/material/styles';
import { useNavigate } from 'react-router-dom';
import MenuIcon from '@mui/icons-material/Menu';
import WbSunnyIcon from '@mui/icons-material/WbSunny';
import NightlightIcon from '@mui/icons-material/Nightlight';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import { ReactComponent as UKIcon } from '../../Images/UK-Flag.svg';
import { ReactComponent as PolandIcon } from '../../Images/Poland-Flag.svg';
import Switch from 'react-switch';
import './Header.js.css';
import { useTranslation } from 'react-i18next';
import HeaderListItems from './HeaderListItems';
import { useRecoilState } from 'recoil';
import { isHeaderOpenAtom } from '../Atoms/HeaderAtoms';
import { useCookies } from 'react-cookie';

export default function Header() {
	const [isDrawerOpen, setIsDrawerOpen] = useRecoilState(isHeaderOpenAtom);
	const [cookies, setCookies] = useCookies();
	const theme = useTheme();
	const { i18n } = useTranslation();
	console.log('i18n', i18n);
	const [useEnglish, setUseEnglish] = useState(i18n.language === 'en');
	const handleChangeTheme = () => {
		if (cookies['isDarkMode'] === 'true' || cookies['isDarkMode'] === undefined) {
			setCookies('isDarkMode', false, {
				path: '/',
				expires: new Date(2147483647 * 1000),
				sameSite: 'strict',
			});
		} else {
			setCookies('isDarkMode', true, {
				path: '/',
				expires: new Date(2147483647 * 1000),
				sameSite: 'strict',
			});
		}
	};

	const languageClick = () => {
		setUseEnglish(!useEnglish);
		if (useEnglish) {
			i18n.changeLanguage('pl');
			setCookies('language', 'pl', {
				path: '/',
				expires: new Date(2147483647 * 1000),
				sameSite: 'strict',
			});
		} else {
			i18n.changeLanguage('en');
			setCookies('language', 'en', {
				path: '/',
				expires: new Date(2147483647 * 1000),
				sameSite: 'strict',
			});
		}
	};

	return (
		<>
			<Box className={'sticky-header'} sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}>
				<Paper className={'header-container'} variant={'elevation'} square={true}>
					<Stack direction={'row'} justifyContent={'flex-start'}>
						<Box
							sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}
						>
							<IconButton
								color="inherit"
								onClick={() => setIsDrawerOpen(!isDrawerOpen)}
								edge="start"
							>
								<MenuIcon />
							</IconButton>
						</Box>
						<Toolbar>
							<Typography variant="h6" noWrap component="div">
								Rental manager
							</Typography>
						</Toolbar>
					</Stack>
					<div
						style={{
							display: 'flex',
							flexDirection: 'row',
							gap: '10px',
						}}
					>
						<button className={'languageButton'} onClick={languageClick}>
							{useEnglish ? (
								<UKIcon className={'languageIcon'} />
							) : (
								<PolandIcon className={'languageIcon'} />
							)}
						</button>
						<Switch
							checked={theme.palette.mode === 'dark'}
							onChange={handleChangeTheme}
							handleDiameter={30}
							onColor={theme.palette.grey['700']}
							onHandleColor={theme.palette.grey['900']}
							offColor={theme.palette.grey['400']}
							offHandleColor={theme.palette.common.white}
							height={38}
							width={80}
							boxShadow="0px 2px 2px rgba(0, 0, 0, 0.5)"
							activeBoxShadow="0px 2px 2px rgba(0, 0, 0, 0.5)"
							uncheckedIcon={
								<div className={'header-unchecked-icon-container'}>
									<WbSunnyIcon className={'header-icon'} />
								</div>
							}
							checkedIcon={
								<div className={'header-checked-icon-container'}>
									<NightlightIcon className={'header-icon'} />
								</div>
							}
						/>
					</div>
				</Paper>
			</Box>
			<Drawer variant="persistent" anchor="left" open={isDrawerOpen}>
				<Toolbar sx={{ marginBottom: '10px' }} />
				<div className={'drawer-paper'}>
					<List>
						<HeaderListItems />
					</List>
				</div>
			</Drawer>
		</>
	);
}
