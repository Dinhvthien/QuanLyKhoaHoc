"use client";

import {
  ActionIcon,
  Badge,
  Button,
  Center,
  Flex,
  Menu,
  Table,
  Text,
} from "@mantine/core";
import useSWR from "swr";
import { SubjectClient, SubjectMapping } from "../../../web-api-client";
import DashboardLayout from "../../../../components/Layout/DashboardLayout";
import Link from "next/link";
import { IconDots } from "@tabler/icons-react";
import { modals } from "@mantine/modals";
import { handleSubmit, useQuery } from "../../../../lib/helper";
import AppPagination from "../../../../components/AppPagination/AppPagination";

// Define the type for a SubjectMapping object with an index signature
interface SubjectMappingWithIndexSignature extends SubjectMapping {
  [key: string]: any;
}

const fields = Object.keys(new SubjectMapping().toJSON());

export default function CourseSubject() {
  const SubjectService = new SubjectClient();

  const query = useQuery();

  const { data, mutate } = useSWR(
    `/api/subjects/${new URLSearchParams(query)}`,
    () =>
      SubjectService.getSubjects(
        query.filters,
        query.sorts,
        query.page ? parseInt(query.page) : 1,
        query.pageSize ? parseInt(query.pageSize) : 10
      )
  );

  const rows = data?.items?.map((item) => (
    <>
      <Table.Tr key={item.name}>
        {fields.map((field) => {
          // Type assertion to tell TypeScript that `item` is of type `SubjectMappingWithIndexSignature`
          return (
            <Table.Td key={field} py={"md"}>
              {field === "isActive" ? (
                item.isActive ? (
                  <Badge>Đang Kích Hoạt</Badge>
                ) : (
                  <Badge color="red">Không Kích Hoạt</Badge>
                )
              ) : (
                (item as SubjectMappingWithIndexSignature)[field]
              )}
            </Table.Td>
          );
        })}
        <Table.Td>
          <Menu shadow="md">
            <Menu.Target>
              <ActionIcon variant="transparent">
                <IconDots />
              </ActionIcon>
            </Menu.Target>

            <Menu.Dropdown>
              <Menu.Item>
                <Link
                  href={`/dashboard/course/subject/${item.id}`}
                  style={{ textDecoration: "none" }}
                >
                  Sửa
                </Link>
              </Menu.Item>
              <Menu.Item
                onClick={() =>
                  modals.openConfirmModal({
                    title: "Xóa Chủ Đề",
                    children: (
                      <Text size="sm">
                        Bạn Chắc Chắn Muốn Xóa? Thao Tác Này Sẽ Không Thể Phục
                        Hồi
                      </Text>
                    ),
                    confirmProps: { color: "red" },
                    labels: { confirm: "Chắc Chắn", cancel: "Hủy" },
                    onConfirm: () =>
                      handleSubmit(() => {
                        SubjectService.deleteSubject(item.id).then(() => {
                          mutate();
                        });
                      }, "Xóa Thành Công"),
                  })
                }
              >
                Xóa
              </Menu.Item>
            </Menu.Dropdown>
          </Menu>
        </Table.Td>
      </Table.Tr>
    </>
  ));

  return (
    <DashboardLayout>
      <Flex my={"sm"} justify={"end"}>
        <Link href={"/course/subject/create"}>
          <Button ms={"auto"} size="xs">
            Tạo Mới
          </Button>
        </Link>
      </Flex>
      <Table layout="fixed">
        <Table.Thead>
          <Table.Tr>
            {fields.map((item) => {
              return <Table.Th key={item}>{item}</Table.Th>;
            })}
          </Table.Tr>
        </Table.Thead>
        <Table.Tbody>{rows}</Table.Tbody>
      </Table>
      <Center py={"xs"}>
        <AppPagination page={data?.pageNumber} total={data?.totalPages} />
      </Center>
    </DashboardLayout>
  );
}
