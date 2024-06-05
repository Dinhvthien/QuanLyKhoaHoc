"use client";

import { IconShoppingCartShare } from "@tabler/icons-react";
import {
  Card,
  Image,
  Text,
  Group,
  Badge,
  Button,
  ActionIcon,
} from "@mantine/core";
import classes from "./CourseCard.module.css";
import { CourseMapping } from "../../app/web-api-client";
import { formatCurrencyVND } from "../../lib/helper";
import Link from "next/link";

const badges = [
  { emoji: "☀️", label: "Sunny weather" },
  { emoji: "🦓", label: "Onsite zoo" },
  { emoji: "🌊", label: "Sea" },
  { emoji: "🌲", label: "Nature" },
  { emoji: "🤽", label: "Water sports" },
];

export function CourseCard({ data }: { data: CourseMapping }) {
  const {
    code,
    imageCourse,
    numberOfPurchases,
    name,
    numberOfStudent,
    price,
    introduce,
    totalCourseDuration,
  } = data;

  const subjects = badges.map((badge) => (
    <Badge variant="light" key={badge.label} leftSection={badge.emoji}>
      {badge.label}
    </Badge>
  ));

  return (
    <Card withBorder radius="md" p="md" className={classes.card}>
      <Card.Section>
        <Image src={imageCourse} alt={imageCourse} height={250} />
      </Card.Section>

      <Card.Section className={classes.section} mt="md">
        <Group justify="apart">
          <Link href={`/${code}`} style={{ textDecoration: "none" }}>
            <Text fz="lg" fw={500}>
              {name}
            </Text>
          </Link>
          <Badge size="sm" variant="light">
            {formatCurrencyVND(price)}
          </Badge>
        </Group>
        <Text fz="sm" mt="xs">
          {introduce}
        </Text>
      </Card.Section>

      <Card.Section className={classes.section}>
        <Text mt="md" className={classes.label} c="dimmed">
          Chủ Đề
        </Text>
        <Group gap={7} mt={5}>
          {subjects}
        </Group>
      </Card.Section>

      <Group mt="xs">
        <Link href={`/${code}`} style={{ flex: 1 }}>
          <Button radius="md" size="xs" w={"100%"}>
            Xem Chi Tiết
          </Button>
        </Link>
        <ActionIcon variant="default" radius="md" size={36}>
          <IconShoppingCartShare className={classes.like} stroke={1.5} />
        </ActionIcon>
      </Group>
    </Card>
  );
}
