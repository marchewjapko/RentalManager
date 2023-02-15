import { Divider, Drawer, IconButton, List, Paper } from '@mui/material';
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
	const theme = useTheme();
	const navigate = useNavigate();
	const { i18n } = useTranslation();
	const [useEnglish, setUseEnglish] = useState(i18n.language === 'en');
	const [isDarkMode, setIsDarkMode] = useCookies();
	const handleChangeTheme = () => {
		if (
			isDarkMode['isDarkMode'] === 'true' ||
			isDarkMode['isDarkMode'] === undefined
		) {
			setIsDarkMode('isDarkMode', false, {
				path: '/',
				expires: new Date(2147483647 * 1000),
				sameSite: 'strict',
			});
		} else {
			setIsDarkMode('isDarkMode', true, {
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
		} else {
			i18n.changeLanguage('en');
		}
	};

	return (
		<div className={'sticky-header'}>
			<Paper
				className={'header-container'}
				variant={'elevation'}
				square={true}
			>
				<div>
					<IconButton
						color="inherit"
						onClick={() => setIsDrawerOpen(true)}
						edge="start"
					>
						<MenuIcon />
					</IconButton>
				</div>
				<div
					style={{
						display: 'flex',
						flexDirection: 'row',
						gap: '10px',
					}}
				>
					<button
						className={'languageButton'}
						onClick={languageClick}
					>
						{useEnglish === true && (
							<UKIcon className={'languageIcon'} />
						)}
						{useEnglish === false && (
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
			<Drawer variant="persistent" anchor="left" open={isDrawerOpen}>
				<Paper
					className={'drawer-paper'}
					variant={'elevation'}
					square={true}
				>
					<div className={'drawer-header'}>
						<div className={'drawer-title'}>System monitor</div>
						<IconButton
							className={'drawer-header-icon-container'}
							onClick={() => setIsDrawerOpen(false)}
						>
							<ChevronLeftIcon fontSize={'large'} />
						</IconButton>
					</div>
					<Divider />
					<List>
						<HeaderListItems />
					</List>
				</Paper>
			</Drawer>
		</div>
	);
}
