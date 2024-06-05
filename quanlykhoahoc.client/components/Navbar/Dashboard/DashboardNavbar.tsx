import { Group, Code, Text } from "@mantine/core";
import { Icon360View, IconBellRinging, IconReceipt2 } from "@tabler/icons-react";
import classes from "./DashboardNavbar.module.css";
import Link from "next/link";
import { usePathname } from "next/navigation";

const data = [
  { link: "/dashboard", label: "Quản Trị", icon: IconBellRinging },
  { link: "/dashboard/course", label: "Khóa Học", icon: Icon360View },
  { link: "/dashboard/course/subject", label: "Chủ Đề", icon: IconReceipt2 },
];

export function DashboardNavbar() {
  const pathname = usePathname();

  const links = data.map((item) => (
    <Link
      className={classes.link}
      data-active={pathname === item.link || undefined}
      href={item.link}
      key={item.label}
    >
      <item.icon className={classes.linkIcon} stroke={1.5} />
      <span>{item.label}</span>
    </Link>
  ));

  return (
    <nav className={classes.navbar}>
      <div className={classes.navbarMain}>
        <Group className={classes.header} justify="space-between">
          <Link href={"/"} style={{ textDecoration: " none" }}>
            <Text fw={"bold"}>
              Trang Chủ
            </Text>
          </Link>

          <Code fw={700}>v1</Code>
        </Group>
        {links}
      </div>
    </nav>
  );
}
