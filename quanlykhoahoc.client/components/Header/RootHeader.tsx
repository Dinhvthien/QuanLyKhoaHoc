"use client";

import {
  Group,
  Button,
  Text,
  Divider,
  Box,
  Burger,
  Drawer,
  ScrollArea,
  rem,
} from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import classes from "./RootHeader.module.css";
import Link from "next/link";
import { usePathname } from "next/navigation";

const mockdata = [
  { label: "Trang Chủ", value: "/" },
  { label: "Giới Thiệu", value: "/about" },
  { label: "Khóa Học", value: "/course" },
  { label: "Quản Trị", value: "/dashboard" },
];

export function RootHeader() {
  const [drawerOpened, { toggle: toggleDrawer, close: closeDrawer }] =
    useDisclosure(false);
  const pathname = usePathname();

  return (
    <Box>
      <header className={classes.header}>
        <Group justify="space-between" h="100%">
          <Link href={"/"} style={{ textDecoration: " none" }}>
            <Text fw={"bold"} color="dark">
              Quản Lý Khóa Học
            </Text>
          </Link>

          <Group h="100%" gap={0} visibleFrom="sm">
            {mockdata.map((item) => {
              return (
                <Link
                  key={item.value}
                  href={item.value}
                  className={classes.link}
                  data-active={pathname === item.value || undefined}
                >
                  {item.label}
                </Link>
              );
            })}
          </Group>

          <Group visibleFrom="sm">
            <Link href={"/login"}>
              <Button size="xs">Đăng Nhập</Button>
            </Link>
          </Group>

          <Burger
            opened={drawerOpened}
            onClick={toggleDrawer}
            hiddenFrom="sm"
          />
        </Group>
      </header>

      <Drawer
        opened={drawerOpened}
        onClose={closeDrawer}
        size="100%"
        padding="md"
        title="Navigation"
        hiddenFrom="sm"
        zIndex={1000000}
      >
        <ScrollArea h={`calc(100vh - ${rem(80)})`} mx="-md">
          <Divider my="sm" />

          <a href="#" className={classes.link}>
            Home
          </a>
          <a href="#" className={classes.link}>
            Learn
          </a>
          <a href="#" className={classes.link}>
            Academy
          </a>

          <Divider my="sm" />

          <Group justify="center" grow pb="xl" px="md">
            <Button variant="default">Log in</Button>
            <Button>Sign up</Button>
          </Group>
        </ScrollArea>
      </Drawer>
    </Box>
  );
}
