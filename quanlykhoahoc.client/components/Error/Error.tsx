import { Title, Text, Button, Container, Group } from "@mantine/core";
import classes from "./Error.module.css";
import Link from "next/link";

export function Error({ status }: { status: number }) {
  return (
    <div className={classes.root}>
      <Container>
        <div className={classes.label}>{status}</div>
        <Title className={classes.title}>Bạn Không Nên Ở Đây</Title>
        <Text size="lg" ta="center" className={classes.description}>
          Trang Bạn Muốn Tìm Không Tồn Tại
        </Text>
        <Group justify="center">
          <Link href={"/"}>
            <Button variant="white" size="md">
              Quay Lại Trang Chủ?
            </Button>
          </Link>
        </Group>
      </Container>
    </div>
  );
}
