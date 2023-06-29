import {AnimatePresence, motion, useCycle} from "framer-motion";
import {MutableRefObject, useEffect, useRef} from "react";
import MenuToggle from "./MenuToggle";
import Navigation from "./Navigation";
import "./NavigationMenu.css"

export const useDimensions = (ref: MutableRefObject<any>) => {
    const dimensions = useRef({width: 0, height: 0});
    useEffect(() => {
        if (ref.current !== null) {
            dimensions.current.width = ref.current.offsetWidth;
            dimensions.current.height = ref.current.offsetHeight;
        }
    }, []);

    return dimensions.current;
};

const sidebar = {
    open: (height = 1000) => ({
        // clipPath: `circle(${height * 2 + 200}px at 40px 40px)`,
        transition: {
            type: "spring",
            stiffness: 20,
            restDelta: 2
        },
    }),
    closed: {
        // clipPath: "circle(0px at 0px 0px)",
        transition: {
            delay: 0.5,
            type: "spring",
            stiffness: 400,
            damping: 40
        },
    }
};

export default function NavigationMenu() {
    const [isOpen, toggleOpen] = useCycle(false, true);
    const containerRef = useRef(null);
    const {height} = useDimensions(containerRef);

    return (
        <motion.div
            initial={false}
            animate={isOpen ? "open" : "closed"}
            custom={height}
            ref={containerRef}
        >
            {/*<motion.div className="navigation-background" variants={sidebar}/>*/}
            {/*<Navigation/>*/}
            <MenuToggle toggle={() => toggleOpen()}/>
            <AnimatePresence>
                {isOpen && (
                    <motion.div key="modal" initial={{opacity: 0}}
                                animate={{opacity: 1}}
                                exit={{opacity: 0}} className={'navigation-background'}>
                        <Navigation/>
                    </motion.div>
                )}
            </AnimatePresence>
        </motion.div>
    );
}